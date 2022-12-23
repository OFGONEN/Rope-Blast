/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace FFStudio
{
    public class LevelManager : MonoBehaviour
    {
#region Fields
      [ Title( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;

      [ Title( "Level Releated" ) ]
        public SharedProgressNotifier notifier_progress;
        public PurchaseSystem system_purchase;
        public RopeBoxData[] ropeBoxData_array;
        public List_Slot shared_list_slot_merge;
        public List_Slot shared_list_slot_launch;
		public SharedStringNotifier notif_save;

		List< Slot > slot_list = new List< Slot >(9); 
#endregion

#region UnityAPI
#endregion

#region API
        // Info: Called from Editor.
        public void LevelLoadedResponse()
        {
			var levelData = CurrentLevelData.Instance.levelData;
            // Set Active Scene.
			if( levelData.scene_overrideAsActiveScene )
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 1 ) );
            else
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 0 ) );
		}

        // Info: Called from Editor.
        public void LevelRevealedResponse()
        {

        }

        // Info: Called from Editor.
        public void LevelStartedResponse()
        {

        }

        public void OnTileCountChanged( int count )
        {
            if( count == 0 )
            {
				SerializeSaveData();
				levelCompleted.Raise();
            }
		}

        public void OnPurchase()
        {
			var index = system_purchase.PurchaseIndex;

			slot_list.Clear();

			for( var i = 0; i < shared_list_slot_merge.itemList.Count; i++ )
            {
				var slot = shared_list_slot_merge.itemList[ i ];

                if( slot.IsEmpty )
                {
					slot_list.Add( slot );
				}
			}

            if( slot_list.Count > 0 )
                ( slot_list.ReturnRandom() as SlotMerge ).SpawnRopeBox( ropeBoxData_array[ index ] );
		}
#endregion

#region Implementation
        void SerializeSaveData()
        {
			notif_save.SharedValue = JsonUtility.ToJson( new SaveData( shared_list_slot_merge, shared_list_slot_launch ) );
		}
#endregion
    }
}

[ System.Serializable ]
public struct SaveData
{
	public int[] slot_merge_data;
	public int[] slot_launch_data;

    public SaveData( List_Slot list_slot_merge, List_Slot list_slot_launch )
    {
		slot_merge_data  = new int[ list_slot_merge.itemDictionary.Count ];
		slot_launch_data = new int[ list_slot_launch.itemDictionary.Count ];

		int counter = 0;

		foreach( var slot in list_slot_merge.itemDictionary.Values )
        {
			slot_merge_data[ counter ] = slot.GetRopeLevel();
			counter++;
		}

		counter = 0;
		foreach( var slot in list_slot_launch.itemDictionary.Values )
		{
			slot_launch_data[ counter ] = slot.GetRopeLevel();
			counter++;
		}
	}
}