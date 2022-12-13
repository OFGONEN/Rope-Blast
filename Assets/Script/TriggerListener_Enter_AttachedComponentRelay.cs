/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;

public class TriggerListener_Enter_AttachedComponentRelay : MonoBehaviour
{
#region Fields
    public UnityEvent< Collider, Component > unityEvent;

    Component _component;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        _component = GetComponent< TriggerListener_Enter >().AttachedComponent;
    }
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		unityEvent.Invoke( collider, _component );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}