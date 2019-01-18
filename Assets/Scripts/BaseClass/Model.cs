using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class Model : MonoBehaviour, IModel
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	protected View CurrentView {
		get;
		set;
	}

	#endregion

	#region FUNCTIONS

	public void SetView(View view)
	{
		CurrentView = view;
	}

	public virtual void Initialize()
	{

	}

	protected T GetView<T>() where T : View
	{
		return CurrentView as T;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
