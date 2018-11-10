using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
	#region MEMBERS

	[Header("[ General ]")]
	[SerializeField]
	private bool isSingleton = true;

	[Header("[ Default control keys ]")]
	[SerializeField]
	private InputAxis[] controlAxes;
	[SerializeField]
	private InputKey[] controlKeys;

	[Header("[ Settings ]")]
	[SerializeField]
	private float keyRepeatDelay = 0.3f;
	[SerializeField]
	private float keyRepeatRate = 8;
	[SerializeField]
	private float axisInputDeadzone = 0.12f;

	[Space(10)]
	[SerializeField]
	private bool enableLog = false;

	#endregion

	#region PROPERTIES

	public static InputManager Instance { get; private set; }

	// GENERAL
	private bool IsSingleton {
		get { return isSingleton; }
	}

	// DEFAULT CONTROL KEYS
	private InputAxis[] ControlAxes {
		get { return controlAxes; }
	}
	private InputKey[] ControlKeys {
		get { return controlKeys; }
	}

	// SETTINGS
	private float KeyRepeatDelay {
		get { return keyRepeatDelay; }
	}
	private float KeyRepeatRate {
		get { return keyRepeatRate; }
	}
	private float AxisInputDeadzone {
		get { return axisInputDeadzone; }
	}

	private bool EnableLog {
		get { return enableLog; }
	}

	// VARIABLES
	private float KeyRepeaterStart { get; set; }
	private int KeyRepeatCount { get; set; }
	private float NextTimeSentSteeringStatisticsSendEvent { get; set; }

	private Dictionary<string, UnityAxis> Axes { get; set; }

	// STATIC
	private static readonly string NONE_AXIS_NAME = "None";
	private static readonly float SECONDS_BETWEEN_STEERING_STATISTICS_SEND = 60.0f;

	#endregion

	#region FUNCTIONS

	// IMANAGER IMPLEMENTATION
	public void SetDefaultData ()
	{
		
	}

	// KEY STATE SETTERS
	public void SetAxisValue (string axisName, float value)
	{
		InputAxis key = GetInputAxisByName(axisName);

		if (key != null)
		{
			key.SetValue(value);
		}
	}

	public void SetKeyValue (string keyName, bool value)
	{
		InputKey key = GetInputKeyByName(keyName);

		if (key != null)
		{
			key.SetState((value == true) ? KeyState.DOWN : KeyState.UP);
		}
	}

	public void SetKeyDown (string keyName, bool oneShot = false)
	{
		InputKey key = GetInputKeyByName(keyName);

		if (key != null)
		{
			key.SetState(KeyState.DOWN);
			key.SetOneShotFlag(oneShot);
		}
	}

	public void SetKeyUp (string keyName)
	{
		InputKey key = GetInputKeyByName(keyName);

		if (key != null)
		{
			key.SetState(KeyState.UP);
		}
	}

	// KEY STATE GETTERS
	public float GetAxis (string axis)
	{
		InputAxis key = GetInputAxisByName(axis);

		return (key != null) ? key.GetValue() : 0f;
	}

	public bool GetKey (string key)
	{
		return GetKeyState(key, KeyState.HOLD);
	}

	public bool GetKeyDown (string key)
	{
		return GetKeyState(key, KeyState.DOWN);
	}

	public bool GetKeyUp (string key)
	{
		return GetKeyState(key, KeyState.UP);
	}

	public bool GetKeyRepeat (string key)
	{
		return GetKeyState(key, KeyState.REPEAT);
	}

	protected virtual void Awake ()
	{
		if (IsSingleton == true)
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}

		CreateAxesDictionary();
	}

	protected void LateUpdate ()
	{
		UpdateAxesStates();
	}

	// STATE CHECKS
	private bool GetKeyState (string keyName, KeyState stateCheck)
	{
		InputKey key = GetInputKeyByName(keyName);
		bool state = false;

		if (key == null)
		{
			Log("The following key: " + keyName + " could not be found in InputManager");

			return state;
		}

		// check touch key status
		KeyState touchKeyState = key.GetState();

		state |= (stateCheck == KeyState.REPEAT) ? GetKeyRepeat(
			touchKeyState == KeyState.DOWN,
			touchKeyState == KeyState.HOLD) : state;

		// check keyboard/gamepad key status
		for (int i = 0; i < key.KeyList.Length; i++)
		{
			state |= CheckKeyPressByState(key.KeyList[i], stateCheck);
		}

		// check axis status
		state |= CheckAxisInputByState(key.AxisName, stateCheck, key.AxisSign);
		state |= (touchKeyState == stateCheck);

		return state;
	}

	private bool CheckKeyPressByState (KeyCode key, KeyState state)
	{
		switch (state)
		{
			case KeyState.HOLD:
				return Input.GetKey(key);
			case KeyState.DOWN:
				return Input.GetKeyDown(key);
			case KeyState.UP:
				return Input.GetKeyUp(key);
			case KeyState.REPEAT:
				return GetKeyRepeat(Input.GetKeyDown(key), Input.GetKey(key));
		}

		return false;
	}

	private bool CheckAxisInputByState (string axis, KeyState state, AxisSign sign)
	{
		if (axis == NONE_AXIS_NAME)
		{
			return false;
		}

		switch (state)
		{
			case KeyState.HOLD:
				return GetUnityAxis(axis, sign);
			case KeyState.DOWN:
				return GetUnityAxisDown(axis, sign);
			case KeyState.UP:
				return GetUnityAxisUp(axis, sign);
			case KeyState.REPEAT:
				return GetKeyRepeat(GetUnityAxisDown(axis, sign), GetUnityAxis(axis, sign));
		}

		return false;
	}

	private InputAxis GetInputAxisByName (string name)
	{
		for (int i = 0; i < ControlAxes.Length; i++)
		{
			if (ControlAxes[i].Name == name)
			{
				return ControlAxes[i];
			}
		}

		Log("The following axis: " + name + " could not be found in InputManager");

		return null;
	}

	private InputKey GetInputKeyByName (string name)
	{
		for (int i = 0; i < ControlKeys.Length; i++)
		{
			if (ControlKeys[i].Name == name)
			{
				return ControlKeys[i];
			}
		}

		Log("The following key: " + name + " could not be found in InputManager");

		return null;
	}

	private bool GetKeyRepeat (bool isKeyDown, bool isKeyHeld)
	{
		// first key input
		if (isKeyDown == true)
		{
			KeyRepeaterStart = Time.unscaledTime + KeyRepeatDelay;
			KeyRepeatCount = 0;

			return true;
		}

		// wait for the repeater delay
		if (Time.unscaledTime < KeyRepeaterStart || isKeyHeld == false)
		{
			return false;
		}

		// repeat key input
		int currentRepeatCount = (int)Mathf.Floor((Time.unscaledTime - KeyRepeaterStart) * KeyRepeatRate);

		if (KeyRepeatCount != currentRepeatCount)
		{
			KeyRepeatCount = currentRepeatCount;

			return true;
		}

		return false;
	}

	private bool GetUnityAxisDown (string axis, AxisSign sign)
	{
		return GetAxisState(axis, sign) == KeyState.DOWN;
	}

	private bool GetUnityAxisUp (string axis, AxisSign sign)
	{
		return GetAxisState(axis, sign) == KeyState.UP;
	}

	private bool GetUnityAxis (string axis, AxisSign sign)
	{
		return GetAxisState(axis, sign) == KeyState.HOLD;
	}

	private KeyState GetAxisState (string axis, AxisSign sign)
	{
		if (axis == string.Empty)
		{
			return KeyState.NONE;
		}

		float axisValue = Input.GetAxisRaw(axis);
		float axisAbsValue = Mathf.Abs(axisValue);

		float lastAxisAbsValue = Mathf.Abs(Axes[axis].LastAxisValue);

		// check if axis returned to neutral position
		if (axisAbsValue < AxisInputDeadzone && lastAxisAbsValue > AxisInputDeadzone)
		{
			return KeyState.UP;
		}

		// check if state is unchanged
		if ((axisValue > 0 && sign == AxisSign.NEGATIVE) ||
			(axisValue < 0 && sign == AxisSign.POSITIVE))
		{
			return KeyState.NONE;
		}

		// check if axis is held in one direction
		if (axisAbsValue > AxisInputDeadzone && lastAxisAbsValue > AxisInputDeadzone)
		{
			return KeyState.HOLD;
		}

		// check if axis has been just moved from neutral position
		if (axisAbsValue > AxisInputDeadzone && lastAxisAbsValue < AxisInputDeadzone)
		{
			return KeyState.DOWN;
		}

		return KeyState.NONE;
	}

	// UPDATED METHODS
	private void UpdateAxesStates ()
	{
		Dictionary<string, UnityAxis>.KeyCollection axesKeys = Axes.Keys;

		foreach (string key in axesKeys)
		{
			UnityAxis axis = Axes[key];

			if (axis != null)
			{
				axis.UpdateAxisRawValue();
			}
		}
	}

	// SETUP
	private void CreateAxesDictionary ()
	{
		Axes = new Dictionary<string, UnityAxis>() { { NONE_AXIS_NAME, null } };

		foreach (InputKey key in controlKeys)
		{
			if (key.AxisName != string.Empty && Axes.ContainsKey(key.AxisName) == false)
			{
				Axes.Add(key.AxisName, new UnityAxis(key.AxisName));
			}
		}
	}

	// TOOLS
	private void Log (object message)
	{
		if (EnableLog == true)
		{
			Debug.Log(message);
		}
	}

	#endregion

	#region CLASS_ENUMS

	[System.Serializable]
	public class InputKey
	{
		#region MEMBERS

		[Header("[ General ]")]
		[SerializeField]
		private string name = "unknown";
		[SerializeField]
		private KeyCode[] keyList;

		[Header("[ Axis ]")]
		[SerializeField]
		private string axisName;
		[SerializeField]
		private AxisSign axisSign;

		#endregion

		#region PROPERTIES

		// GENERAL
		public string Name {
			get { return name; }
		}
		public KeyCode[] KeyList {
			get { return keyList; }
		}

		// AXIS
		public string AxisName {
			get { return axisName; }
		}
		public AxisSign AxisSign {
			get { return axisSign; }
		}

		// VARIABLES (used for on screen buttons)
		public KeyState TouchState { get; private set; }

		private int LastStateChangeFrame { get; set; }
		private int FramesSinceLastStateChange {
			get { return Time.frameCount - LastStateChangeFrame; }
		}

		private bool IsOneShot { get; set; }

		#endregion

		#region FUNCTIONS

		public InputKey ()
		{
			keyList = new KeyCode[1];
		}

		public void SetOneShotFlag (bool state)
		{
			IsOneShot = state;
		}

		public void SetState (KeyState state)
		{
			if (state != TouchState)
			{
				TouchState = state;
				LastStateChangeFrame = Time.frameCount;
			}
		}

		public KeyState GetState ()
		{
			KeyState output = KeyState.NONE;

			// handle one shot flag
			if (IsOneShot == true && FramesSinceLastStateChange >= 1 && TouchState == KeyState.DOWN)
			{
				TouchState = KeyState.UP;
				output = KeyState.UP;

				IsOneShot = false;
			}

			// handle key down and hold
			if (TouchState == KeyState.DOWN)
			{
				if (FramesSinceLastStateChange == 0)
				{
					output = KeyState.DOWN;
				}
				else
				{
					output = KeyState.HOLD;
				}
			}

			// handle key up and disable
			if (TouchState == KeyState.UP && FramesSinceLastStateChange == 0)
			{
				output = KeyState.UP;
			}

			return output;
		}

		#endregion
	}

	[System.Serializable]
	public class InputAxis
	{
		#region MEMBERS

		[SerializeField]
		private string name = "unknown";
		[SerializeField]
		[Tooltip("Name used for the axis in Unity Input [optional]")]
		private string internalName = "unknown";
		[SerializeField]
		private bool invert = false;

		#endregion

		#region PROPERTIES

		// GENERAL
		public string Name {
			get { return name; }
		}
		public string InternalName {
			get { return internalName; }
		}
		public bool Invert {
			get { return invert; }
			private set { invert = value; }
		}

		// VARIABLES (used for on screen buttons)
		private float CurrentValue { get; set; }

		private int LastStateChangeFrame { get; set; }
		private int ChangeCount { get; set; }

		#endregion

		#region FUNCTIONS

		public InputAxis ()
		{

		}

		public void InvertAxis (bool state = true)
		{
			Invert = state;
		}

		public void SetValue (float value)
		{
			if (LastStateChangeFrame != Time.frameCount)
			{
				ChangeCount = 1;
				CurrentValue = value;
			}
			else
			{
				ChangeCount++;
				CurrentValue += value;
			}

			LastStateChangeFrame = Time.frameCount;
		}

		public float GetValue ()
		{
			float unityAxis = (InternalName != string.Empty) ? Input.GetAxis(InternalName) : 0f;
			// float output = (ChangeCount != 0) ? CurrentValue / ChangeCount : 0f;
			float output = Mathf.Clamp(CurrentValue + unityAxis, -1f, 1f);

			return output * (Invert ? -1f : 1f);
		}

		#endregion
	}

	public class UnityAxis
	{
		#region MEMBERS

		#endregion

		#region PROPERTIES

		public string Name { get; private set; }
		public float LastAxisValue { get; private set; }

		#endregion

		#region FUNCTIONS

		public UnityAxis (string name)
		{
			Name = name;
		}

		public void UpdateAxisRawValue ()
		{
			LastAxisValue = Input.GetAxisRaw(Name);
		}

		#endregion
	}

	public enum KeyState
	{
		NONE,
		HOLD,
		DOWN,
		UP,
		REPEAT
	}

	public enum AxisSign
	{
		POSITIVE,
		NEGATIVE
	}

	#endregion

}