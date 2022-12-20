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
#endregion

#region Properties
#endregion

#region Unity API
	protected override void OnDisable()
	{
		base.OnDisable();
		pool_ropeBox.ReturnEntity( slot_ropeBox );
	}
#endregion

#region API
	public void SpawnRopeBox( RopeBoxData data )
	{
		slot_ropeBox = pool_ropeBox.GetEntity();
		slot_ropeBox.transform.SetParent( slot_dragged_transform );

		slot_isEmpty = false;
		slot_ropeBox.Spawn( data, slot_dragged_transform.position );
	}
#endregion

#region Implementation
	protected override void OnDropSameSlot()
	{
		slot_dragged_transform.localPosition = Vector3.zero;
		slot_collider.enabled                = true;
	}

	protected override void MergeRopeBox( RopeBox incoming )
	{
		var sequence = recycledSequence.Recycle( () => {
			incoming.DeSpawn();
			OnMergeRopeBoxDone();
		} );
		sequence.Append( incoming.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	protected override void CacheRopeBox( RopeBox incoming )
	{
		slot_ropeBox = incoming;

		var sequence = recycledSequence.Recycle( OnCacheRopeBoxDone );
		sequence.Append( slot_ropeBox.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	protected override void OnCacheRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy           = false;
	}

	protected override void OnMergeRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy = false;

		var data = slot_ropeBox.RopeBoxData.NextRopeBoxData;
		slot_ropeBox.DeSpawn();

		SpawnRopeBox( data );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}