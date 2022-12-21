/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class TileRow : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Tiles" ), SerializeField ] Tile[] tile_array;
    [ LabelText( "Tile Table Move Event" ), SerializeField ] GameEvent event_tile_table_move;

    int tile_count;
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
        for( var i = 0; i < tile_array.Length; i++ )
			tile_array[ i ].Cracked = OnTileCracked;
    }
#endregion

#region API
#endregion

#region Implementation
    void OnTileCracked()
    {
		tile_count++;

        if( tile_count == tile_array.Length )
			event_tile_table_move.Raise();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	void CacheTiles()
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );

		tile_array = new Tile[ transform.childCount ];

		for( var i = 0; i < tile_array.Length; i++ )
		{
			tile_array[ i ] = transform.GetChild( i ).GetComponent< Tile >();
		}
	}
#endif
#endregion
}