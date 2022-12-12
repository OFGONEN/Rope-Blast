/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_ropeBox_data", menuName = "FF/Game/Rope Box Data" ) ]
public class RopeBoxData : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "RopeBox Rope Data " ) ] RopeData ropeBox_rope;
    [ LabelText( "RopeBox Mesh" ) ] Mesh ropeBox_mesh;
    [ LabelText( "RopeBox Material" ) ] Material ropeBox_material;
    [ LabelText( "RopeBox UI Color ( Maybe We Won't Use This )" ) ] Color ropeBox_ui_color;
#endregion
}