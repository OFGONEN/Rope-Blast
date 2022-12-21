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
    [ LabelText( "Rope Collider" ), SerializeField ] Collider rope_collider;
    [ LabelText( "Rope Renderer" ), SerializeField ] Renderer rope_renderer;
    [ LabelText( "Rope Hook's Mesh Filter" ), SerializeField ] MeshFilter rope_hook_meshFilter;
    [ LabelText( "Rope Hook's Renderer" ), SerializeField ] Renderer rope_hook_renderer;
    // [ LabelText( "Particle Spawner" ), SerializeField ] ParticleSpawner particle_spawner; //todo: Enable This ?

	public RopeData RopeData => rope_data;
	public bool IsBusy       => rope_tile_list.Count > 0;

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
		UpdateRope( ropeData );

		rope_collider.enabled      = true;
		rope_renderer.enabled      = true;
		rope_hook_renderer.enabled = true;

		rope_end.position = rope_end_position_default;

		Launch();
	}

	public void UpdateRope( RopeData ropeData )
	{
		rope_data = ropeData;

		//todo: We may need to change collider position and size according to rope hook's mesh size 
		//todo: But this should not happen, If it does we need to change the mesh of the hook model.

		rope_renderer.sharedMaterial      = ropeData.RopeMaterial;
		rope_hook_meshFilter.mesh         = ropeData.RopeHookMesh;
		rope_hook_renderer.sharedMaterial = ropeData.RopeHookMaterial;
	}

	public void DeSpawn()
	{
		recycledSequence.Kill();

		rope_collider.enabled      = false;
		rope_renderer.enabled      = false;
		rope_hook_renderer.enabled = false;
	}

    //Info: Editor Call from TriggerListener_Enter
    public void OnTileTrigger( Collider tileCollider )
    {
        var tile = tileCollider.GetComponent< TriggerListener >().AttachedComponent as Tile;
		tile.GetDamage( rope_data.RopeDamage );

		if( tile.Health < 0 )
			PierceTile( tile );
        else
            DamageTile( tile );
	}
#endregion

#region Implementation
    void PierceTile( Tile tile )
    {
		//todo: spawn pierced pfx ?
		rope_tile_list.Add( tile );
	}

    void DamageTile( Tile tile )
    {
		//todo: spawn can't pierce pfx ?
		Return();
	}

	void AttachAllTiles()
	{
		for( var i = 0; i < rope_tile_list.Count; i++ )
			rope_tile_list[ i ].GetAttached( rope_end );
	}

    void Launch()
    {
		rope_tile_list.Clear();

		var launchDelta = ( rope_data.RopeLength - 1 ) * GameSettings.Instance.rope_launch_length_delta + GameSettings.Instance.rope_launch_delta;

		var launchPosition = transform.position + transform.forward * launchDelta;
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

		AttachAllTiles();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}