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
    [ ShowInInspector, ReadOnly ] int movement_count;

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
			transform.DOMove( movement_count * GameSettings.Instance.tile_table_movement_delta * -transform.up,
			GameSettings.Instance.tile_table_movement_duration )
			.SetRelative()
			.SetEase( GameSettings.Instance.tile_table_movement_ease ),
			OnTableMoveComplete
		);

		movement_count = 0;
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	void AddSpaceBetweenTileRows( float space )
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );

		for( var i = 1; i < transform.childCount; i++ )
		{
			transform.GetChild( i ).localPosition = Vector3.up * ( i - 1 ) * space;
		}
	}
#endif
#endregion
}