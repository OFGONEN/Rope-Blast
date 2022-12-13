/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_rope_data", menuName = "FF/Game/Rope Data" ) ]
public class RopeData : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Rope Level" ), SerializeField ] int rope_level;
    [ LabelText( "Rope Reach Length" ), SerializeField ] int rope_length;
    [ LabelText( "Rope Damage" ), SerializeField ] float rope_damage;
    [ LabelText( "Rope Launch Delay" ), SerializeField ] float rope_reach_delay;
    [ LabelText( "Rope Launch Duration" ), SerializeField ] float rope_reach_duration;
    [ LabelText( "Rope Return Delay" ), SerializeField ] float rope_return_delay;
    [ LabelText( "Rope Return Duration" ), SerializeField ] float rope_return_duration;

    public int RopeLevel            => rope_level;
    public int RopeLength           => rope_length;
    public float RopeDamage         => rope_damage;
    public float RopeReachDelay     => rope_reach_delay;
    public float RopeReachDuration  => rope_reach_duration;
    public float RopeReturnDelay    => rope_return_delay;
    public float RopeReturnDuration => rope_return_duration;
#endregion
}