/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

//Info: There is a edge case where the Tile can be attached to a Rope and TileTable is moving down. This will cause the position of the Tile on the Rope. We will let this happen and detach the Tile from TileTable when GetAttached method is called.
public class Tile : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Tile Health" ), SerializeField ] float tile_health;
    [ LabelText( "Tile Destory Currency" ), SerializeField ] Vector2 tile_currency;
    [ LabelText( "Tile Hit Currency" ), SerializeField ] Vector2 tile_currency_hit;
    [ LabelText( "Currency" ), SerializeField ] Currency notif_currency;

  [ Title( "Components" ) ]
    [ LabelText( "Tile Collider" ), SerializeField ] Collider tile_collider;
    [ LabelText( "Tile CrackSetter" ), SerializeField ] CrackSetter tile_crackSetter;
    // [ LabelText( "ParticleSpawnner" ), SerializeField ] ParticleSpawner _particleSpawner;

// Property
    public float Health => tile_health_current;
    public UnityMessage Cracked 
    {
        set
        {
			onCracked = value;
		}
    }

// Private
    [ ShowInInspector, ReadOnly ] float tile_health_current;
    UnityMessage onCracked;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		tile_health_current = tile_health;
		onCracked           = ExtensionMethods.EmptyMethod;
	}
#endregion

#region API
    public bool GetDamage( float damage ) // Return true if this Tile is cracked
    {
		tile_health_current -= damage;

		var crackedProgress = Mathf.InverseLerp( tile_health, 0, tile_health_current );
		var cracked         = tile_health < 0;

		notif_currency.SharedValue += tile_currency_hit.ReturnRandom();

		//todo: Spawn a Particle effect ?
		tile_crackSetter.SetCrackProgress( crackedProgress );

		return cracked;
	}

    public void GetAttached( Transform parent )
    {
		onCracked();
		// tile_collider.enabled = false;
		transform.SetParent( parent );
	}

    public void OnLaunchTableCollide()
    {
		// _particleSpawner.Spawn( 0 );
        //todo: Spawn a UI Particle effect ?

		gameObject.SetActive( false );
		transform.SetParent( null );

		notif_currency.SharedValue += tile_currency.ReturnRandom();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
