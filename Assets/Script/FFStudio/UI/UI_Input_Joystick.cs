/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class UI_Input_Joystick : UIEntity
{
#region Fields
[ Title( "Setup" ) ] 
    public RectTransform image_base;
    public RectTransform image_stick;
    public SharedInput_JoyStick input_JoyStick;
#endregion

#region Properties
#endregion

#region Unity API
    void OnEnable()
    {
		input_JoyStick.input_toggle.Subscribe( InputToggle );
	}

    void OnDisable()
    {
		input_JoyStick.input_toggle.Unsubscribe( InputToggle );
	}

	void Awake()
	{
		image_base.gameObject.SetActive( false );
	}
#endregion

#region API
#endregion

#region Implementation
    void InputToggle()
    {
		var enabled = input_JoyStick.Input_Enabled;
		image_base.gameObject.SetActive( input_JoyStick.Input_Enabled );

		if( enabled )
		{
			input_JoyStick.Subscribe( InputChange );

			var position             = input_JoyStick.Input_Screen_Position;
			    position.y           = uiTransform.position.y;
			    uiTransform.position = position;
		}
        else
			input_JoyStick.Unsubscribe( InputChange );
	}

    void InputChange()
    {
		image_stick.anchoredPosition = image_base.anchoredPosition + input_JoyStick.SharedValue * GameSettings.Instance.ui_Entity_JoyStick_Gap;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
