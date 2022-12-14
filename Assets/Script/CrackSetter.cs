/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class CrackSetter : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    [ SerializeField ] Texture2D[] crack_texture_array;
    [ SerializeField ] MeshRenderer tileRenderer;

	static readonly int SHADER_ID_TEXTURE_CRACK = Shader.PropertyToID( "_Crack_Texture" );
	MaterialPropertyBlock materialPropertyBlock;
#endregion

#region Unity API
    void Awake()
    {
		materialPropertyBlock = new MaterialPropertyBlock();
	}
#endregion

#region API
    [ Button ]
    public void Execute( int crackIndex )
    {
		tileRenderer.GetPropertyBlock( materialPropertyBlock );
		materialPropertyBlock.SetTexture( SHADER_ID_TEXTURE_CRACK, crack_texture_array[ crackIndex ] );
		tileRenderer.SetPropertyBlock( materialPropertyBlock );
	}
#endregion

#region Implementation
#endregion
}
