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
    [ LabelText( "Rope Launch Delay" ), SerializeField ] float rope_delay;
    [ LabelText( "Rope Launch Duration" ), SerializeField ] float rope_duration_reach;
    [ LabelText( "Rope Return Duration" ), SerializeField ] float rope_duration_return;

    public int RopeLevel            => rope_level;
    public int RopeLength           => rope_length;
    public float RopeDamage         => rope_damage;
    public float RopeDelay          => rope_delay;
    public float RopeDurationReach  => rope_duration_reach;
    public float RopeDurationReturn => rope_duration_return;
#endregion
}