using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviourSingleton<GUIManager>
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private MainMenuController mainMenu;
	[SerializeField]
	private PauseMenuController pauseMenu;
	[SerializeField]
	private GameHUDController inGameHUD;
	[SerializeField]
	private GameOverScreenController gameOverScreen;

	#endregion

	#region PROPERTIES

	// REFERENCES
	public MainMenuController MainMenu {
		get {return mainMenu;}
	}
	public PauseMenuController PauseMenu {
		get {return pauseMenu;}
	}
	public GameHUDController InGameHUD {
		get {return inGameHUD;}
	}
	public GameOverScreenController GameOverScreen {
		get {return gameOverScreen;}
	}

	#endregion

	#region FUNCTIONS

	public void SetMainMenuState (bool state = true, bool hideOther = false)
	{
		SetGUIState(MainMenu.gameObject, state, hideOther);
	}

	public void SetPauseMenuState (bool state = true, bool hideOther = false)
	{
		SetGUIState(PauseMenu.gameObject, state, hideOther);
	}

	public void SetInGameHUDState (bool state = true, bool hideOther = false)
	{
		SetGUIState(InGameHUD.gameObject, state, hideOther);
	}

	public void SetGameOverScreenState (bool state = true, bool hideOther = false)
	{
		SetGUIState(GameOverScreen.gameObject, state, hideOther);
	}

	public void SetGUIState (GameObject target, bool state, bool hideOther)
	{
		if (hideOther == true)
		{
			HideAll();
		}

		target.SetActive(state);
	}

	public void HideAll ()
	{
		MainMenu.gameObject.SetActive(false);
		PauseMenu.gameObject.SetActive(false);
		InGameHUD.gameObject.SetActive(false);
		GameOverScreen.gameObject.SetActive(false);
	}

	protected override void Awake()
	{
		base.Awake();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
