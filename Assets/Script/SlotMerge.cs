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
#endregion

#region API
	public void SpawnRopeBox( RopeBoxData data )
	{
		slot_ropeBox = pool_ropeBox.GetEntity();
		slot_ropeBox.transform.SetParent( slot_dragged_transform );

		slot_isEmpty = true;
		slot_ropeBox.Spawn( data, slot_dragged_transform.position );
	}

	public override void TransferRopeBox( RopeBox incoming )
	{
		// incoming.transform.parent = ?;
	}
#endregion

#region Implementation
	protected override void OnDropSameSlot()
	{
		slot_dragged_transform.localPosition = Vector3.zero;
		slot_collider.enabled                = true;
	}

	protected override void OnDropDifferentSlot()
	{
		// If the paired slot is not empty but it has a maxed level rope box or a rope box with a different leveled rope
		if( !slot_pair.IsEmpty && slot_pair.RopeBoxData.NextRopeBoxData == null && slot_pair.RopeBoxData.RopeLevel != slot_ropeBox.RopeBoxData.RopeLevel )
			OnDropSameSlot();
		else
		{
			slot_isEmpty = true;
			slot_ropeBox = null;
			slot_pair.TransferRopeBox( slot_ropeBox );
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}