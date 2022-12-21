/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class Slot : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField, LabelText( "Slot List All" ) ] protected List_Slot shared_list_slot_all;
    [ SerializeField, LabelText( "Slot List Custom" ) ] protected List_Slot shared_list_slot_custom;
    [ SerializeField, LabelText( "RopeBox Pool" ) ] protected Pool_RopeBox pool_ropeBox;

  [ Title( "Components" ) ]
    [ SerializeField, LabelText( "Slot's Dragged Transform" ) ] protected Transform slot_dragged_transform;
    [ SerializeField, LabelText( "Slot Selection Collider" ) ] protected Collider slot_collider;

    public virtual bool IsBusy     => slot_isBusy;
    public bool IsEmpty            => slot_isEmpty;
    public RopeBoxData RopeBoxData => slot_ropeBox.RopeBoxData;
// Private
	protected RecycledSequence recycledSequence = new RecycledSequence();

	[ ShowInInspector, ReadOnly ] protected bool slot_isBusy;
    [ ShowInInspector, ReadOnly ] protected bool slot_isEmpty = true;
	[ ShowInInspector, ReadOnly ] protected Slot slot_pair;
	[ ShowInInspector, ReadOnly ] protected RopeBox slot_ropeBox;
#endregion

#region Properties
#endregion

#region Unity API
	void OnEnable()
	{
		shared_list_slot_all.AddList( this );
		shared_list_slot_custom.AddList( this );
	}

	protected virtual void OnDisable()
	{
		shared_list_slot_all.RemoveList( this );
		shared_list_slot_custom.RemoveList( this );	
	}
#endregion

#region API
    public bool OnSelect()
    {
		return !IsEmpty && !IsBusy;
	}

    public void OnDeSelect()
	{
		if( slot_pair == this || 
			Vector3.Distance( slot_dragged_transform.position.SetY( 0 ), 
				slot_pair.transform.position ) < GameSettings.Instance.selection_pair_distance   )
			OnDropSameSlot();
		else
			OnDropDifferentSlot();
		
		slot_pair = null;
		slot_dragged_transform.localPosition = Vector3.zero;
	}

    public virtual void OnSnatch()
	{
		slot_collider.enabled = false;
		slot_pair             = this;
	}

    public void OnDragUpdate( Vector3 position )
	{
		slot_dragged_transform.position = position;

		float closestDistance = float.MaxValue;

		for( var i = 0; i < shared_list_slot_all.itemList.Count; i++ )
		{
			var distance = Vector3.Distance( slot_dragged_transform.position.SetZ( 0 ), shared_list_slot_all.itemList[ i ].transform.position.SetZ( 0 ) );

			if( distance < closestDistance )
			{
				slot_pair = shared_list_slot_all.itemList[ i ];
				closestDistance = distance;
			}
		}
	}

	public void TransferRopeBox( RopeBox incoming )
	{
		slot_isEmpty = false;
		slot_isBusy  = true;

		incoming.transform.parent = slot_dragged_transform;

		if( slot_ropeBox == null )
			CacheRopeBox( incoming );
		else
			MergeRopeBox( incoming );
	}

    protected bool CanDropDifferentSlot()
	{
		return slot_pair.IsEmpty || ( slot_pair.RopeBoxData.NextRopeBoxData != null && slot_ropeBox.RopeBoxData.RopeLevel == slot_pair.RopeBoxData.RopeLevel );
	}

	protected void OnDropDifferentSlot()
	{
		// If the paired slot is not empty but it has a maxed level rope box or a rope box with a different leveled rope
		if( CanDropDifferentSlot() )
		{
			slot_pair.TransferRopeBox( slot_ropeBox );

			slot_isBusy  = false;
			slot_isEmpty = true;
			slot_ropeBox = null;
		}
		else
			OnDropSameSlot();

	}

	protected virtual void CacheRopeBox( RopeBox incoming )
	{
		slot_ropeBox = incoming;

		var sequence = recycledSequence.Recycle( OnCacheRopeBoxDone );
		sequence.Append( slot_ropeBox.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	protected virtual void OnCacheRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy = false;
	}

	protected abstract void OnDropSameSlot();
	protected abstract void MergeRopeBox( RopeBox incoming );
	protected abstract void OnMergeRopeBoxDone();
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}