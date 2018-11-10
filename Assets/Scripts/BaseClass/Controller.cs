using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class Controller : MonoBehaviour, IController
{
	#region MEMBERS

	[SerializeField]
	private View view;
	[SerializeField]
	private Model model;

	#endregion

	#region PROPERTIES

	public virtual bool IsEnabled {
		get { return gameObject.activeSelf && gameObject.activeInHierarchy; }
	}

	protected View CurrentView {
		get { return view; }
		set { view = value; }
	}

	protected Model CurrentModel {
		get { return model; }
		set { model = value; }
	}

	#endregion

	#region FUNCTIONS

	public void SetupModelAndView()
	{
		if (CurrentView != null && CurrentModel != null)
		{
			CurrentView.SetModel(CurrentModel);
			CurrentModel.SetView(CurrentView);
		}
	}

	public void SetView(View view)
	{
		CurrentView = view;
	}

	public void SetModel(Model model)
	{
		CurrentModel = model;
	}

	public virtual void Initialize()
	{
		CurrentModel.Initialize();
		CurrentView.Initialize();
	}

	public virtual void EnableController()
	{
		gameObject.SetActiveOptimized(true);
	}

	public virtual void DisableController()
	{
		gameObject.SetActiveOptimized(false);
	}

	public virtual void ToggleController()
	{
		if (gameObject.activeSelf == true)
		{
			DisableController();
		}
		else
		{
			EnableController();
		}
	}

	protected virtual void Awake()
	{
		SetupModelAndView();
	}

	protected virtual void Start()
	{
		Initialize();
	}

	protected T GetModel<T>() where T : Model
	{
		return CurrentModel as T;
	}

	protected T GetView<T>() where T : View
	{
		return CurrentView as T;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
