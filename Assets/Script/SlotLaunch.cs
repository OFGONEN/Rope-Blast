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
	[ BoxGroup( "Components" ), LabelText( "Rope" ), SerializeField ] Rope slot_rope;

    public override bool IsBusy => slot_isBusy || slot_rope.IsBusy;
    public override RopeBoxData RopeBoxData => slot_ropeBoxData;
// Private
	[ ShowInInspector, ReadOnly ] RopeBoxData slot_ropeBoxData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public override void OnSnatch()
	{
		base.OnSnatch();

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

		slot_rope.Spawn( slot_ropeBoxData.RopeData );

		slot_ropeBox.DeSpawn();
		slot_ropeBox = null;
	}

	protected override void MergeRopeBox( RopeBox incoming )
	{
		slot_ropeBox = incoming;

		var sequence = recycledSequence.Recycle( OnMergeRopeBoxDone );

		sequence.Append( incoming.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	protected override void OnMergeRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy = false;

		slot_ropeBoxData = slot_ropeBox.RopeBoxData.NextRopeBoxData;
		slot_rope.UpdateRope( slot_ropeBoxData.RopeData );

		slot_ropeBox.DeSpawn();
		slot_ropeBox = null;
	}

	protected override void OnCacheRopeBoxDone()
	{
		base.OnCacheRopeBoxDone();

		slot_ropeBoxData = slot_ropeBox.RopeBoxData;

		slot_ropeBox.DeSpawn();
		slot_ropeBox = null;

		slot_rope.Spawn( slot_ropeBoxData.RopeData );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
