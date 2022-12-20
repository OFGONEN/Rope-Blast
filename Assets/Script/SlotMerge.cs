/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
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
#endregion

#region Implementation
	protected override void OnDropSameSlot()
	{
		slot_dragged_transform.localPosition = Vector3.zero;
		slot_collider.enabled                = true;
	}

	protected override void OnDropDifferentSlot()
	{
		if( slot_pair.RopeBoxData.RopeLevel != slot_ropeBox.RopeBoxData.RopeLevel )
			OnDropSameSlot();
		else
		{
			slot_ropeBox.DeSpawn();
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}