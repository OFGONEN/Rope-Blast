/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class SlotMerge : Slot
{
#region Fields
    [ BoxGroup( "Shared" ), SerializeField, LabelText( "Merge Table Is Empty" ) ] protected SharedBoolNotifier notif_table_empty;
#endregion

#region Properties
#endregion

#region Unity API
	protected override void OnDisable()
	{
		base.OnDisable();

		if( slot_ropeBox != null )
			pool_ropeBox.ReturnEntity( slot_ropeBox );
	}
#endregion

#region API
	[ Button() ]
	public void SpawnRopeBox( RopeBoxData data )
	{
		slot_ropeBox = pool_ropeBox.GetEntity();
		slot_ropeBox.transform.SetParent( slot_dragged_transform );

		slot_isEmpty = false;
		slot_ropeBox.Spawn( data, slot_dragged_transform.position );

		CheckIfTableIsFull();
	}
	#endregion

	#region Implementation
	protected override void DropDifferentSlot()
	{
		base.DropDifferentSlot();
		notif_table_empty.SharedValue = true;
	}
	protected override void OnDropSameSlot()
	{
		slot_dragged_transform.localPosition = Vector3.zero;
		slot_collider.enabled                = true;
		slot_pair                            = null;
	}

	protected override void MergeRopeBox( RopeBox incoming )
	{
		var sequence = recycledSequence.Recycle( () => {
			incoming.DeSpawn();
			OnMergeRopeBoxDone();
		} );

		sequence.Append( incoming.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );

		notif_table_empty.SharedValue = true;
	}

	protected override void OnMergeRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy = false;

		var data = slot_ropeBox.RopeBoxData.NextRopeBoxData;
		slot_ropeBox.DeSpawn();

		SpawnRopeBox( data );

		_particleSpawner.Spawn( 0 );
	}

	void CheckIfTableIsFull()
	{
		var tableIsEmpty = false;

		for( var i = 0; i < shared_list_slot_custom.itemList.Count; i++ )
		{
			tableIsEmpty = tableIsEmpty || shared_list_slot_custom.itemList[ i ].IsEmpty;
		}

		notif_table_empty.SharedValue = tableIsEmpty;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}