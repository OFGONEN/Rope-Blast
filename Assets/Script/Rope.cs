/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Rope : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ LabelText( "Rope's End" ), SerializeField ] Transform rope_end;
    [ LabelText( "Rope Collider" ), SerializeField ] Collider _collider;
    [ LabelText( "Rope Renderer" ), SerializeField ] Renderer _renderer;

// Private 
    Vector3 rope_end_position_default;
    RopeData rope_data;

    List< Tile > rope_tile_list = new List< Tile >( 8 );

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		rope_end_position_default = rope_end.position;
	}
#endregion

#region API
    public void Spawn( RopeData ropeData )
    {
		rope_data = ropeData;

		_collider.enabled = true;
		_renderer.enabled = true;

		rope_end.position = rope_end_position_default;

		Launch();
	}

    public void OnTileTrigger( Collider tileCollider )
    {
        var tile = tileCollider.GetComponent< TriggerListener >().AttachedComponent as Tile;

        if( rope_data.RopeDamage > tile.Health )
			AttachTile( tile );
        else
            DamageTile( tile );

	}
#endregion

#region Implementation
    void AttachTile( Tile tile )
    {

    }

    void DamageTile( Tile tile )
    {

    }

    void Launch()
    {
		var launchDelta = ( rope_data.RopeLength - 1 ) * GameSettings.Instance.rope_launch_length_delta + GameSettings.Instance.rope_launch_delta;

		var launchPosition = transform.position + GameSettings.Instance.game_forward * launchDelta;
		var duration = Vector3.Distance( rope_end.position, launchPosition ) / rope_data.RopeLaunchSpeed;

		var sequence = recycledSequence.Recycle( Return );
		sequence.AppendInterval( rope_data.RopeLaunchDelay );
		sequence.Append( rope_end.DOMove( launchPosition, duration )
		.SetEase( rope_data.RopeLaunchEase ) );
	}

    void Return()
    {
		var duration = Vector3.Distance( rope_end.position, rope_end_position_default );

		var sequence = recycledSequence.Recycle( Launch );
		sequence.AppendInterval( rope_data.RopeReturnDelay );
		sequence.Append( rope_end.DOMove( rope_end_position_default, duration )
		.SetEase( rope_data.RopeLaunchEase ) );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}