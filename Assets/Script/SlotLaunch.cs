/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class SlotLaunch : Slot
{
#region Fields
  [ Title( "Components" ) ]
	[ LabelText( "Rope" ), SerializeField ] Rope slot_rope;


    public override bool IsBusy => slot_isBusy || slot_rope.IsBusy;
// Private
	RopeBoxData slot_ropeBoxData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public override void OnSnatch()
	{
		slot_collider.enabled = false;
		slot_pair             = this;

		slot_rope.DeSpawn();

		slot_ropeBox = pool_ropeBox.GetEntity();
		slot_ropeBox.transform.SetParent( slot_dragged_transform );

		slot_ropeBox.Spawn( slot_ropeBoxData, slot_dragged_transform.position );
	}
#endregion

#region Implementation
	protected override void OnDropSameSlot()
	{
		slot_isBusy = true;

		var sequence = recycledSequence.Recycle( OnDropSameSlotDone );
		sequence.Append( slot_dragged_transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1,
			GameSettings.Instance.ropeBox_jump_duration ).SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	void OnDropSameSlotDone()
	{
		slot_isBusy = false;
		slot_collider.enabled = true;

		slot_ropeBox.DeSpawn();
		slot_rope.Spawn( slot_ropeBoxData.RopeData );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	protected override void CacheRopeBox(RopeBox incoming)
	{
		throw new System.NotImplementedException();
	}

	protected override void MergeRopeBox(RopeBox incoming)
	{
		throw new System.NotImplementedException();
	}

	protected override void OnCacheRopeBoxDone()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnDropDifferentSlot()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnMergeRopeBoxDone()
	{
		throw new System.NotImplementedException();
	}
}
