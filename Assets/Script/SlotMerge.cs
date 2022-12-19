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
#endregion

#region Implementation
	protected override void OnDropLaunchSlot()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnDropMergeSlot()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnDropSameSlot()
	{
		slot_dragged_transform.position = _position;
		slot_collider.enabled           = true;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}