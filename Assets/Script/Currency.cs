/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "notif_currency", menuName = "FF/Game/Currency" ) ]
public class Currency : SharedFloatNotifier
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Load()
    {
		PlayerPrefsUtility.Instance.GetFloat( ExtensionMethods.Key_Currency, 0 );
	}

    public void Save()
    {
		PlayerPrefsUtility.Instance.SetFloat( ExtensionMethods.Key_Currency, sharedValue );
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}