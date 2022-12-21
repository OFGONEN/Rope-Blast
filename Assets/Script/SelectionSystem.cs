/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_selection", menuName = "FF/Game/System/Selection" ) ]
public class SelectionSystem : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Main Camera Reference" ), SerializeField ] SharedReferenceNotifier notif_camera_reference;
    [ LabelText( "Shared Finger" ), SerializeField ] SharedLeanFinger shared_finger;

	Vector2 finger_position;
	Slot _slot;

	Camera _camera;
	int layer_mask;

	UnityMessage onFingerDown;
    UnityMessage onFingerUp;
    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    //Info: EditorCall
    public void OnLevelRevealed()
    {
		SetLayerMaskToSlot();
		_camera = ( notif_camera_reference.sharedValue as Transform ).GetComponent< Camera >();

		onFingerDown = TryToSelectSlot;
	}

    //Info: EditorCall
    public void OnFingerDown()
    {
		onFingerDown();
	}

    //Info: EditorCall
    public void OnFingerUp()
    {
		onFingerUp();
	}

    //Info: Call on manager_asset Update 
    public void OnUpdate()
    {
		onUpdate();
	}

    //Info: Call on manager_asset Awake 
    public void EmptyDelegates()
    {
		onFingerDown = ExtensionMethods.EmptyMethod;
		onFingerUp   = ExtensionMethods.EmptyMethod;
		onUpdate     = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Implementation
    void TryToSelectSlot()
    {
		onFingerDown = ExtensionMethods.EmptyMethod;
		onFingerUp   = FingerUp;

		finger_position = shared_finger.ScreenPosition;

		var worldPosition_Start = _camera.ScreenToWorldPoint( finger_position.ConvertV3( _camera.nearClipPlane ) );
		var worldPosition_End   = _camera.ScreenToWorldPoint( finger_position.ConvertV3( _camera.farClipPlane ) );

		RaycastHit hitInfo;
		var hit = Physics.Raycast( worldPosition_Start, ( worldPosition_End - worldPosition_Start ).normalized, out hitInfo, GameSettings.Instance.selection_distance, layer_mask );

		// Debug.DrawRay( worldPosition_Start, ( worldPosition_End - worldPosition_Start ).normalized * GameSettings.Instance.selection_distance, Color.red, 1 );

		if( !hit ) return; // Return if no hit

		_slot = hitInfo.collider.GetComponent< ComponentHost >().HostComponent as Slot;

		if( _slot.OnSelect() )
		{
			_slot.OnSnatch();

			SetLayerMaskToSelectionTable();
			DragSlot();
			onUpdate = DragSlot;
		}
	}

	void FingerUp()
	{
		EmptyDelegates();
		onFingerDown = TryToSelectSlot;

		_slot.OnDeSelect();
		SetLayerMaskToSlot();
	}

	void DragSlot()
	{
		finger_position = shared_finger.ScreenPosition;

		var worldPosition_Start = _camera.ScreenToWorldPoint( finger_position.ConvertV3( _camera.nearClipPlane ) );
		var worldPosition_End   = _camera.ScreenToWorldPoint( finger_position.ConvertV3( _camera.farClipPlane ) );

		RaycastHit hitInfo;
		var hit = Physics.Raycast( worldPosition_Start, ( worldPosition_End - worldPosition_Start ).normalized, out hitInfo, GameSettings.Instance.selection_distance, layer_mask );

		// Debug.DrawRay( worldPosition_Start, ( worldPosition_End - worldPosition_Start ).normalized * GameSettings.Instance.selection_distance, Color.red, 1 );

		_slot.OnDragUpdate( hitInfo.point.OffsetY( GameSettings.Instance.selection_height ) );
	}

	void SetLayerMaskToSlot()
	{
		layer_mask = 1 << GameSettings.Instance.selection_layer_slot;
	}
	
	void SetLayerMaskToSelectionTable()
	{
		layer_mask = 1 << GameSettings.Instance.selection_layer_table;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}