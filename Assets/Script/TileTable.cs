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
	[ SerializeField ] GameEvent event_level_complete;

    int movement_count_total;
    int movement_count_current;

	int tileRow_count;

	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		tileRow_count = transform.childCount;
	}
#endregion

#region API
	//Info: Editor Call
    public void OnTableMove()
    {
		movement_count_total   += 1;
		movement_count_current += 1;

        if( !recycledTween.IsPlaying() )
			MoveTable();
	}
#endregion

#region Implementation
    void OnTableMoveComplete()
    {
        if( movement_count_current > 0 )
			MoveTable();
		else if( movement_count_total == tileRow_count )
			event_level_complete.Raise();
	}

    void MoveTable()
    {
		recycledTween.Recycle(
			transform.DOMove( movement_count_current * GameSettings.Instance.tile_table_movement_delta * -transform.forward,
			GameSettings.Instance.tile_table_movement_duration )
			.SetRelative()
			.SetEase( GameSettings.Instance.tile_table_movement_ease ),
			OnTableMoveComplete
		);

		movement_count_current = 0;
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}