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

    Transform camera_transform;
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
		layer_mask       = 1 << GameSettings.Instance.selection_layer;
		camera_transform = notif_camera_reference.sharedValue as Transform;

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
		RaycastHit hitInfo;
		var hit = Physics.Raycast( camera_transform.position, camera_transform.forward, out hitInfo, GameSettings.Instance.selection_distance, layer_mask );

		if( !hit ) return; // Return if no hit

		var slot = hitInfo.collider.GetComponent< ComponentHost >().HostComponent as Slot;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}