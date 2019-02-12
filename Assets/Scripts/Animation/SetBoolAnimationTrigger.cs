using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolAnimationTrigger : MonoBehaviour
{
	public Animator targetAnimator;
	public string targetName = "unknown";
	
	public void SetAnimatorBoolTrue ()
	{
		targetAnimator.SetBool(targetName, true);
	}
	
	public void SetAnimatorBoolFalse ()
	{
		targetAnimator.SetBool(targetName, false);
	}
}
