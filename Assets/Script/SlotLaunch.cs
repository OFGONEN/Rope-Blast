/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class SlotLaunch : Slot
{
#region Fields
	[ BoxGroup( "Components" ), LabelText( "Rope" ), SerializeField ] Rope slot_rope;
	[ BoxGroup( "Components" ), LabelText( "Rope Selection Icon" ), SerializeField ] Image slot_rope_selection;
	[ BoxGroup( "Components" ), LabelText( "Rope Level Icon" ), SerializeField ] Image slot_rope_icon;
	[ BoxGroup( "Shared" ), LabelText( "Purchase System" ), SerializeField ] PurchaseSystem system_purchase;

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
    public override int OnSnatch()
	{
		base.OnSnatch();

		slot_rope.DeSpawn();

		slot_ropeBox = pool_ropeBox.GetEntity();
		slot_ropeBox.transform.SetParent( slot_dragged_transform );

		slot_ropeBox.Spawn( slot_ropeBoxData, slot_dragged_transform.position );

		slot_rope_icon.enabled = false;

		return RopeBoxData.RopeLevel;
	}

	public void SpawnRope( RopeBoxData ropeBoxData )
	{
		slot_collider.enabled = true;
		slot_isBusy = false;
		slot_isEmpty = false;
		slot_pair = null;

		slot_ropeBoxData = ropeBoxData;
		slot_ropeBox = null;

		slot_rope.SpawnWithoutLaunch( slot_ropeBoxData.RopeData );

		slot_rope_icon.enabled = true;
		slot_rope_icon.sprite  = system_purchase.GetPurchaseLevel( slot_ropeBoxData.RopeLevel - 1 );
	}

	public void OnLevelStarted()
	{
		if( !slot_isEmpty ) slot_rope.Launch();
	}
#endregion

#region Implementation
	protected override void OnDropSameSlot()
	{
		slot_pair   = null;
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

		slot_rope_icon.enabled = true;
		slot_rope_icon.sprite = system_purchase.GetPurchaseLevel( slot_ropeBoxData.RopeLevel - 1 );
	}

	protected override void MergeRopeBox( RopeBox incoming )
	{
		slot_ropeBox = incoming;

		var sequence = recycledSequence.Recycle( OnMergeRopeBoxDone );

		sequence.Append( incoming.transform.DOLocalJump( Vector3.zero, GameSettings.Instance.ropeBox_jump_power, 1, GameSettings.Instance.ropeBox_jump_duration )
			.SetEase( GameSettings.Instance.ropeBox_jump_ease ) );
	}

	protected override void CacheRopeBox( RopeBox incoming )
	{
		base.CacheRopeBox( incoming );

		slot_ropeBoxData = incoming.RopeBoxData;
	}

	protected override void OnMergeRopeBoxDone()
	{
		slot_collider.enabled = true;
		slot_isBusy = false;

		slot_ropeBoxData = slot_ropeBox.RopeBoxData.NextRopeBoxData;
		slot_rope.UpdateRope( slot_ropeBoxData.RopeData );

		slot_ropeBox.DeSpawn();
		slot_ropeBox = null;

		_particleSpawner.Spawn( 0 );
		
		slot_rope_icon.sprite = system_purchase.GetPurchaseLevel( slot_ropeBoxData.RopeLevel - 1 );
	}

	protected override void OnCacheRopeBoxDone()
	{
		base.OnCacheRopeBoxDone();

		slot_ropeBoxData = slot_ropeBox.RopeBoxData;

		slot_ropeBox.DeSpawn();
		slot_ropeBox = null;

		slot_rope.Spawn( slot_ropeBoxData.RopeData );

		slot_rope_icon.enabled = true;
		slot_rope_icon.sprite = system_purchase.GetPurchaseLevel( slot_ropeBoxData.RopeLevel - 1 );
	}

	public override void OnOtherSlotSelected( int slotLevel )
	{
		if( !slot_isEmpty && RopeBoxData.RopeLevel != slotLevel ) return;

		slot_rope_selection.color = GameSettings.Instance.slot_launch_selectionColor_positive;
	}

	public override void OnOtherSlotDeSelected()
	{
		slot_rope_selection.color = GameSettings.Instance.slot_launch_selectionColor_default;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
