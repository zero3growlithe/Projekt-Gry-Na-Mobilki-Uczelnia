using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiplierCurve
{
	#region MEMBERS
	
	[SerializeField]
	private AnimationCurve curve;
	[SerializeField]
    private float valueMultiplier;
	[SerializeField]
    private float timeMultiplier;

	#endregion

	#region PROPERTIES

    public AnimationCurve Curve {
    	get {return curve;}
    }
    public float ValueMultiplier {
    	get {return valueMultiplier;}
    }
    public float TimeMultiplier {
    	get {return timeMultiplier;}
    }

	#endregion

	#region FUNCTIONS

    public MultiplierCurve ()
    {
    	curve = AnimationCurve.Linear(0, 0, 1, 1);
		valueMultiplier = 1;
		timeMultiplier = 1;
    }

    /// <summary>
    /// t - time, v - value, maxTime - time scale, maxValue - valueScale
    /// </summary>
    public MultiplierCurve (float t1, float v1, float t2, float v2, float maxValue = 1f, float maxTime = 1f)
    {
    	curve = AnimationCurve.Linear(t1, v1, t2, v2);
		valueMultiplier = maxValue;
		timeMultiplier = maxTime;
    }

    public float Evaluate (float time)
    {
    	if (TimeMultiplier == 0)
    	{
    		return 0;
    	}

    	return Curve.Evaluate(time / TimeMultiplier) * ValueMultiplier;
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
