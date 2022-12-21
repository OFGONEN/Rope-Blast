/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TileTable : MonoBehaviour
{
#region Fields
    int movement_count;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	//Info: Editor Call
    public void OnTableMove()
    {
		movement_count += 1;

        if( !recycledTween.IsPlaying() )
			MoveTable();
	}
#endregion

#region Implementation
    void OnTableMoveComplete()
    {
        if( movement_count > 0 )
			MoveTable();

	}

    void MoveTable()
    {
		recycledTween.Recycle(
			transform.DOMove( movement_count * GameSettings.Instance.tile_table_movement_delta * -transform.forward,
			GameSettings.Instance.tile_table_movement_duration )
			.SetRelative()
			.SetEase( GameSettings.Instance.tile_table_movement_ease ),
			OnTableMoveComplete
		);
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}