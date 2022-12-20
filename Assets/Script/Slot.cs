/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public abstract class Slot : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField, LabelText( "Slot List All" ) ] protected List_Slot shared_list_slot_base;
    [ SerializeField, LabelText( "Slot List Custom" ) ] protected List_Slot shared_list_slot_custom;

  [ Title( "Components" ) ]
    [ SerializeField, LabelText( "Slot's Dragged Transform" ) ] protected Transform slot_dragged_transform;
    [ SerializeField, LabelText( "Slot Selection Collider" ) ] protected Collider slot_collider;

    public bool IsBusy  => slot_isBusy;
    public bool IsEmpty => slot_isEmpty;
// Private
    protected bool slot_isBusy;
    protected bool slot_isEmpty;
	protected Vector3 _position; 

	protected UnityMessage onDeselect;
#endregion

#region Properties
#endregion

#region Unity API
	void OnEnable()
	{
		shared_list_slot_base.AddList( this );
		shared_list_slot_custom.AddList( this );
	}

	void OnDisable()
	{
		shared_list_slot_base.RemoveList( this );
		shared_list_slot_custom.RemoveList( this );	
	}

	private void Awake()
	{
		onDeselect = ExtensionMethods.EmptyMethod;
	}
#endregion

#region API
    public bool OnSelect()
    {
		return !slot_isEmpty && !slot_isBusy;
	}

    public void OnDeSelect()
	{
		onDeselect();
		onDeselect = ExtensionMethods.EmptyMethod;
	}

    public virtual void OnSnatch()
	{
		slot_collider.enabled = false;
		onDeselect            = OnDropSameSlot;
		_position             = slot_dragged_transform.position;
	}

    public virtual void OnDragUpdate( Vector3 position )
	{
		slot_dragged_transform.position = position;
	}

    protected abstract void OnDropLaunchSlot();
    protected abstract void OnDropMergeSlot();
	protected abstract void OnDropSameSlot();
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}