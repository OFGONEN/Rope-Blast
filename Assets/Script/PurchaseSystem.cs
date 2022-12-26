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
    [ SerializeField, LabelText( "Purchase Level Range" ) ] int[] purchase_level_range; //Info: This array's length must be 1 less then total RopeBoxData count
    [ SerializeField, LabelText( "Purchase Context" ) ] Sprite[] purchase_context_array; 

	public int PurchaseCount => purchase_count;
	public int PurchaseIndex => purchase_index;
	public int PurchaseCeil => purchase_level_range[ Mathf.Min( purchase_index, purchase_level_range.Length - 1 ) ];

	[ ShowInInspector ] int purchase_count;
    [ ShowInInspector ] int purchase_index;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Load()
    {
		purchase_count = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_Purchase_Count, 0 );

		FindPurchaseIndex();
	}

    public void Save()
    {
        PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_Purchase_Count, purchase_count );
    }

    //  (int)(Purchase Count ^ 1.25) - Purchase Count
    public float GetPurchaseCost()
    {
		return purchase_cost_base + Mathf.Pow( purchase_count, 1.25f ) - purchase_count;
	}

	public Sprite GetPurchaseContext()
	{
		return purchase_context_array[ purchase_index ];
	}

	public Sprite GetPurchaseContext( int index )
	{
		return purchase_context_array[ index ];
	}

    public void IncreasePurchaseCount()
    {
		purchase_count++;
		FindPurchaseIndex();

		PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_Purchase_Count, purchase_count );
    }
#endregion

#region Implementation
    void FindPurchaseIndex()
    {
		purchase_index = 0;

		for( var i = purchase_level_range.Length - 1; i >= 0; i-- )
        {
            if( purchase_count >= purchase_level_range[ i ] )
            {
				purchase_index = i + 1;
				break;
			}
		}
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	void LogPurchaseCost()
	{
		FFLogger.Log( "Cost: " + GetPurchaseCost() );
	}
#endif
#endregion
}