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
    [ LabelText( "RopeBox Rope Data " ), SerializeField ] RopeData ropeBox_rope;
    [ LabelText( "RopeBox Mesh" ), SerializeField ] Mesh ropeBox_mesh;
    [ LabelText( "RopeBox Material" ), SerializeField ] Material ropeBox_material;
    [ LabelText( "RopeBox UI Image" ), SerializeField ] Sprite ropeBox_ui_image;

    public RopeData RopeData        => ropeBox_rope;
    public Mesh RopeBoxMesh         => ropeBox_mesh;
    public Material RopeBoxMaterial => ropeBox_material;
    public Sprite RopeBoxUIImage    => ropeBox_ui_image;
#endregion
}