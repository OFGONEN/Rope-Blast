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
    [ SerializeField, LabelText( "Slot List" ) ] List_Slot shared_list_slot_base;

  [ Title( "Components" ) ]
    [ SerializeField, LabelText( "Slot's Dragged Transform" ) ] Transform slot_dragged_transform;
    [ SerializeField, LabelText( "Slot Selection Collider" ) ] Collider slot_collider;

    public bool IsBusy  => slot_isBusy;
    public bool IsEmpty => slot_isEmpty;
// Private
    bool slot_isBusy;
    bool slot_isEmpty;

#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public bool OnSelect()
    {
		return !slot_isEmpty && !slot_isBusy;
	}

    public void OnSnatch()
	{
		slot_collider.enabled = false;
		// slot_dragged_transform.position = position;
	}

    public void OnDragUpdate( Vector3 position )
	{
		slot_dragged_transform.position = position;
	}

    public abstract void OnDeSelect();
    public abstract void OnDropLaunchSlot();
    public abstract void OnDropMergeSlot();
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}