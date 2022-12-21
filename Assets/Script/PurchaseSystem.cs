/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_purchase", menuName = "FF/Game/System/Purchase" ) ]
public class PurchaseSystem : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Base Purchase Cost" ) ] float purchase_cost_base;
    [ SerializeField, LabelText( "Purchase Level Range" ) ] int[] purchase_level_range;

	public int PurchaseCount => purchase_count;
	public int PurchaseIndex => purchase_index;

	int purchase_count;
    int purchase_index;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Load()
    {
		purchase_count = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_Purchase_Count, 0 );

        for( var i = 0; i < purchase_level_range.Length; i++ )
        {
            if( purchase_count < purchase_level_range[ i ] )
            {
				purchase_index = i;
				break;
			}
		}
	}

    public void Save()
    {
        PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_Purchase_Count, purchase_count );
    }
#endregion

#region Implementation
    //  (int)(Purchase Count ^ 1.25) - Purchase Count
    public float GetPurchaseCost()
    {
		return purchase_cost_base + Mathf.Pow( purchase_count, 1.25f ) - purchase_count;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}