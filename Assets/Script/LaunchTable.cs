/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTable : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTileTrigger( Collider tileCollider )
    {
        var tile = tileCollider.GetComponent< ComponentHost >().HostComponent as Tile;
		tile.OnLaunchTableCollide();

	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
