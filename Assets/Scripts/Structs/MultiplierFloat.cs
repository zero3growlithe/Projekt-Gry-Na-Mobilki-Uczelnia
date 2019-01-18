using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiplierFloat
{
    [SerializeField]
    private float startValue;
    [SerializeField]
    private float multiplier;
    
    public float Value {
        get {return startValue * multiplier;}
        set {startValue = value;}
    }

    public float Multiplier {
        get {return multiplier;}
        set {multiplier = value;}
    }

    public MultiplierFloat ()
    {
		startValue = 0;
        multiplier = 1;
    }
}
