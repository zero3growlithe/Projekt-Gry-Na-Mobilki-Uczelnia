using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class View : MonoBehaviour, IView
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	protected Model CurrentModel {
		get;
		set;
	}
		
	#endregion

	#region FUNCTIONS

	public void SetModel(Model model)
	{
		CurrentModel = model;
	}

	public virtual void Initialize()
	{
		
	}

	protected T GetModel<T>() where T : Model
	{
		return CurrentModel as T;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
