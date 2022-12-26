/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "level_creator", menuName = "FF/Game/Tool/Level Creator" ) ]
public class LevelCreator : ScriptableObject
{
#region Fields
    [ SerializeField ] string level_code;
    [ FoldoutGroup( "Setup" ), SerializeField ] GameObject tile_row;
    [ FoldoutGroup( "Setup" ), SerializeField ] GameObject[] tile_array;
    [ FoldoutGroup( "Setup" ), SerializeField ] float tile_row_height;
    [ FoldoutGroup( "Setup" ), SerializeField ] float tile_row_count;
    [ FoldoutGroup( "Setup" ), SerializeField ] float tile_start;
    [ FoldoutGroup( "Setup" ), SerializeField ] float tile_gap;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void CreateLevel()
    {
        if( level_code == null || level_code == string.Empty ) FFLogger.LogError( "Level Code is Invalid" );

		EditorSceneManager.MarkAllScenesDirty();

		var tileTable = GameObject.Find( "tile_table" ) as GameObject;
		tileTable.transform.DestroyAllChildren( 1 );

		for( var i = 0; i < level_code.Length; )
        {
            if( level_code[ i ] < 48 || level_code[ i ] > 57 )
            {
                FFLogger.LogError( "Invalid Character At: " + i );
				break;
			}

			var tileRow = PrefabUtility.InstantiatePrefab( tile_row ) as GameObject;
			tileRow.name = "tile_row_" + ( i / tile_row_count );

			tileRow.transform.SetParent( tileTable.transform );
			tileRow.transform.localPosition    = Vector3.up * ( i / tile_row_count ) * tile_row_height;
			tileRow.transform.localEulerAngles = Vector3.zero;


			var tileCount = 0;
			for( ; i < level_code.Length && tileCount < tile_row_count; i++ )
            {
			    var tile = PrefabUtility.InstantiatePrefab( tile_array[ int.Parse( level_code[ i ].ToString() ) - 1 /* NOTE: Due to array index and tile level name difference */] ) as GameObject;
			    tile.name = "tile_" + i;

				tile.transform.SetParent( tileRow.transform );
				tile.transform.localPosition    = Vector3.right * ( tile_start + tileCount * tile_gap );
				tile.transform.localEulerAngles = Vector3.zero;

				tileCount++;
			}

			tileRow.GetComponent< TileRow >().CacheTiles();
		}
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}