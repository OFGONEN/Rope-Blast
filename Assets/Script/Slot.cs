/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Slot : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField, LabelText( "Slot List" ) ] List_Slot shared_list_slot_base;

  [ Title( "Components" ) ]
    [ SerializeField, LabelText( "Slot's Dragged Transform" ) ] Transform slot_dragged_transform;

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
    public abstract bool OnSelect();
    public abstract void OnSnatch();
    public abstract void OnDragUpdate();
    public abstract void OnFingerUp();
    public abstract void OnFingerDown();
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