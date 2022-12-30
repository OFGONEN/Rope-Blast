/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Tutorial : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ SerializeField ] Transform tutorial_hand_transform;

  [ Title( "Tutorial Setup" ) ]
    [ SerializeField ] int tile_explode_count;
    [ SerializeField ] List_Slot shared_list_slot_launch;
    [ SerializeField ] float hand_movement_offset_vertical;
    [ SerializeField ] float hand_movement_speed_lateral;
    [ SerializeField ] Ease hand_movement_ease_lateral;
    [ SerializeField ] Ease hand_movement_ease_vertical;

	[ ShowInInspector, ReadOnly ] SlotLaunch slot_launch_empty;
	[ ShowInInspector, ReadOnly ] SlotLaunch slot_launch_occupied;

	List< SlotLaunch > slot_launch_empty_list;
	List< SlotLaunch > slot_launch_occupied_list;

	RecycledSequence recycledSequence = new RecycledSequence();
	int tile_explode_count_current;

	UnityMessage onTileExplode;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		onTileExplode = TileExplode;
	}
#endregion

#region API
    public void OnTileExplode()
    {
		onTileExplode();
	}
#endregion

#region Implementation
    void TileExplode()
    {
		tile_explode_count_current++;

        if( tile_explode_count == tile_explode_count_current )
        {
			onTileExplode = StopTutorial;
			SpawnTutorialHand();
		}
    }

    void SpawnTutorialHand()
    {
		tutorial_hand_transform.gameObject.SetActive( true );

        slot_launch_empty_list    = new List< SlotLaunch >( 4 );
        slot_launch_occupied_list = new List< SlotLaunch >( 4 );

		for( var i = 0; i < shared_list_slot_launch.itemList.Count; i++ )
		{
			var slotLaunch = shared_list_slot_launch.itemList[ i ] as SlotLaunch;

			if( slotLaunch.IsEmpty )
				slot_launch_empty_list.Add( slotLaunch );
            else
				slot_launch_occupied_list.Add( slotLaunch );
		}

		slot_launch_empty    = slot_launch_empty_list.ReturnRandom();
		slot_launch_occupied = slot_launch_occupied_list.ReturnRandom();

		StartTutorial();
	}

    void StartTutorial()
    {
		FFLogger.Log( "Start Tutorial", this );
		SetHandPosition();

		var sequence = recycledSequence.Recycle( StartTutorial );

		var handPosition     = tutorial_hand_transform.position;
		var targetPosition   = slot_launch_empty.transform.position;
		var durationLateral  = Mathf.Abs( ( targetPosition.x - handPosition.x ) ) / hand_movement_speed_lateral;

		FFLogger.Log( "Hand: " + handPosition + " Target: " + targetPosition + " Duration: " + durationLateral, this );

		sequence.Append( tutorial_hand_transform.DOMoveX( targetPosition.x, durationLateral )
			.SetEase( hand_movement_ease_lateral ) );
		sequence.Join( tutorial_hand_transform.DOMoveY( targetPosition.y + hand_movement_offset_vertical, durationLateral / 2f )
			.SetEase( hand_movement_ease_vertical ) );
		sequence.Join( tutorial_hand_transform.DOMoveY( targetPosition.y, durationLateral / 2f )
			.SetEase( hand_movement_ease_vertical ).SetDelay( durationLateral / 2f ) );

		sequence.OnUpdate( OnTutorialUpdate );
	}

    void StopTutorial()
    {
        FFLogger.Log( "Stop Tutorial", this );
		tutorial_hand_transform.gameObject.SetActive( false );
		recycledSequence.Kill();
	}

	void OnTutorialUpdate()
	{
		bool isAllEmpty = true;
		for( var i = 0; i < slot_launch_empty_list.Count; i++ )
		{
			isAllEmpty = isAllEmpty && slot_launch_empty_list[ i ].IsEmpty;
		}

		bool isAllOccopied = false;
		for( var i = 0; i < slot_launch_occupied_list.Count; i++ )
		{
			isAllOccopied = isAllOccopied || slot_launch_occupied_list[ i ].IsEmpty;
		}

		if( !isAllEmpty && isAllOccopied )
			StopTutorial();
	}

	void SetHandPosition()
	{
		tutorial_hand_transform.position = slot_launch_occupied.transform.position;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}