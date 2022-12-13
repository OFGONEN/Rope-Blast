/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;

[ CreateAssetMenu( fileName = "shared_rope_data", menuName = "FF/Game/Rope Data" ) ]
public class RopeData : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Rope Level" ), SerializeField ] int rope_level;
    [ LabelText( "Rope Material" ), SerializeField ] Material rope_material;
    [ LabelText( "Rope Hook's Mesh" ), SerializeField ] Mesh rope_hook_mesh;
    [ LabelText( "Rope Hook's Material" ), SerializeField ] Material rope_hook_material;
  [ Title( "Launch & Return" ) ]
    [ LabelText( "Rope Reach Length" ), SerializeField ] int rope_length;
    [ LabelText( "Rope Damage" ), SerializeField ] float rope_damage;
    [ LabelText( "Rope Launch Delay" ), SerializeField ] float rope_launch_delay;
    [ LabelText( "Rope Launch Speed" ), SerializeField ] float rope_launch_speed;
    [ LabelText( "Rope Launch Ease" ), SerializeField ] Ease rope_launch_ease;
    [ LabelText( "Rope Return Delay" ), SerializeField ] float rope_return_delay;
    [ LabelText( "Rope Return Speed" ), SerializeField ] float rope_return_speed;
    [ LabelText( "Rope Return Ease" ), SerializeField ] Ease rope_return_ease;

    public int RopeLevel            => rope_level;
    public int RopeLength           => rope_length;
    public float RopeDamage         => rope_damage;
    public float RopeLaunchDelay    => rope_launch_delay;
    public float RopeLaunchSpeed    => rope_launch_speed;
    public Ease RopeLaunchEase      => rope_launch_ease;
    public float RopeReturnDelay    => rope_return_delay;
    public float RopeReturnSpeed    => rope_return_speed;
    public Ease RopeReturnEase      => rope_return_ease;
#endregion
}