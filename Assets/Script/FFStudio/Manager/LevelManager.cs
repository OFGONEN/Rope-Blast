/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

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
        public List_Slot shared_list_slot_merge;
        public RopeBoxData[] ropeBoxData_array;
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

        public void OnPurchase()
        {
			var index = system_purchase.PurchaseIndex;
			system_purchase.IncreasePurchaseCount();

			for( var i = 0; i < shared_list_slot_merge.itemList.Count; i++ )
            {
				var slot = shared_list_slot_merge.itemList[ i ];

                if( slot.IsEmpty )
                {
					( slot as SlotMerge ).SpawnRopeBox( ropeBoxData_array[ index ] );
					break;
				}
			}
		}
#endregion

#region Implementation
#endregion
    }
}