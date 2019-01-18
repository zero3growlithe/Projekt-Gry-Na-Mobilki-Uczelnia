using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDLivesCounter : UIImageCounter
{
    #region MEMBERS

    [SerializeField]
    private ClassValue sourceValue;

    #endregion

    #region PROPERTIES

    private ClassValue SourceValue {
    	get {return sourceValue;}
    }

    #endregion

    #region FUNCTIONS

    protected void Update ()
    {
    	SetActiveByCount((int)SourceValue.DoubleValue);
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
