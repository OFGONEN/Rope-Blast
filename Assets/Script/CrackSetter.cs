/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class CrackSetter : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    [ SerializeField ] Texture2D[] crack_texture_array;
    [ SerializeField ] MeshRenderer tileRenderer;

	MaterialPropertyBlock materialPropertyBlock;
	
	static readonly int SHADER_ID_KEYWORD_APPLY_CRACK = Shader.PropertyToID( "_Apply_Crack" );
	static readonly int SHADER_ID_TEXTURE_CRACK       = Shader.PropertyToID( "_Crack_Texture" );
	static readonly int SHADER_ID_COLOR               = Shader.PropertyToID( "_Crack_Color" );
#endregion

#region Unity API
    void Awake()
    {
		materialPropertyBlock = new MaterialPropertyBlock();
	}
#endregion

#region API
	public void SetCrackProgress( float progress ) // Progress should be between 0 and 1
	{
		var index = Mathf.RoundToInt( Mathf.Lerp( 0, crack_texture_array.Length - 1, progress ) );
		ChangeCrackLevel( index );
	}

    [ Button ]
    public void ChangeCrackLevel( int crackIndex )
    {
		tileRenderer.GetPropertyBlock( materialPropertyBlock );
		materialPropertyBlock.SetTexture( SHADER_ID_TEXTURE_CRACK, crack_texture_array[ crackIndex ] );
		materialPropertyBlock.SetFloat( SHADER_ID_KEYWORD_APPLY_CRACK, 1 );
		tileRenderer.SetPropertyBlock( materialPropertyBlock );
	}
	
	[ Button ]
	public void RemoveCrack()
	{
		tileRenderer.GetPropertyBlock( materialPropertyBlock );
		materialPropertyBlock.SetFloat( SHADER_ID_KEYWORD_APPLY_CRACK, 0 );
		tileRenderer.SetPropertyBlock( materialPropertyBlock );
	}
	
	[ Button ]
	public void ChangeCrackColor( Color color )
	{
		tileRenderer.GetPropertyBlock( materialPropertyBlock );
		materialPropertyBlock.SetColor( SHADER_ID_COLOR, color );
		tileRenderer.SetPropertyBlock( materialPropertyBlock );
	}
#endregion

#region Implementation
#endregion
}
