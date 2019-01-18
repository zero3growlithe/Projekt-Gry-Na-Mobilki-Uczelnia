using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLimitValue : ClassValue
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	public override double DoubleValue {
		get { return CurrentDoubleValue / LimitDoubleValue; }
	}
	public override string StringValue {
		get { return DoubleValue.ToString(); }
	}

	public virtual double CurrentDoubleValue {
		get { return 0; }
	}
	public virtual double LimitDoubleValue {
		get { return 0; }
	}
	public virtual string CurrentStringValue {
		get { return "None"; }
	}
	public virtual string LimitStringValue {
		get { return "None"; }
	}

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
