/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Lean.Touch;

[ CreateAssetMenu( fileName = "shared_input_finger_position", menuName = "FF/Game/Finger Position" ) ]
public class SharedLeanFinger : SharedData< LeanFinger > 
{
    LeanFinger finger;

	public LeanFinger Finger => finger;

	public void SetLeanFinger( LeanFinger finger )
    {
		this.finger = finger;
	}
}