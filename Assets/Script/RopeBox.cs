/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;

public class RopeBox : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ LabelText( "Rope Box Pool" ), SerializeField ] Pool_RopeBox pool_ropeBox;

  [ Title( "Components" ) ]
    [ LabelText( "Rope Box Renderer" ) ] Renderer _renderer;
    [ LabelText( "Rope Box Mesh Filter" ) ] MeshFilter _meshFilter;
    // [ LabelText( "Rope Box UI Text" ) ] TextMeshProUGUI _textRenderer;
    // [ LabelText( "Rope Box UI Image" ) ] Image _imageRenderer;

    public RopeBoxData RopeBoxData => ropeBox_data;
// Private
    RopeBoxData ropeBox_data;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( RopeBoxData ropeBoxData, Vector3 position )
    {
		gameObject.SetActive( true );
		transform.position = position;

		ropeBox_data = ropeBoxData;
	}

    public void DeSpawn()
    {
		pool_ropeBox.ReturnEntity( this );
	}
#endregion

#region Implementation
    void ConvertRopeBox()
    {
		_meshFilter.mesh         = ropeBox_data.RopeBoxMesh;
		_renderer.sharedMaterial = ropeBox_data.RopeBoxMaterial;

        //todo: Enable this after visual design is determined.
		// _textRenderer.text   = "Lvl " + ropeBox_data.RopeData.RopeLevel;
		// _imageRenderer.color = ropeBox_data.RopeBoxUIColor;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}