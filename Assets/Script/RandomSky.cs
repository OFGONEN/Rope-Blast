/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Imphenzia;

public class RandomSky : MonoBehaviour
{
#region Fields
    [ SerializeField ] GradientSkyCamera gradientSkyObject;
#endregion

#region Properties
#endregion

#region Unity API
    void Start()
    {
		gradientSkyObject.gradient = GameSettings.Instance.sky_gradient_array.ReturnRandom();
		gradientSkyObject.CreateOrGetChildObject();
	}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}