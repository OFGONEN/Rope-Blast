/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class SlotLaunch : Slot
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	public override void TransferRopeBox( RopeBox incoming )
	{

	}
#endregion

#region Implementation
	protected override void OnDropDifferentSlot()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnDropSameSlot()
	{
		throw new System.NotImplementedException();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
