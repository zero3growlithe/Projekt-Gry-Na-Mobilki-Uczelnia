using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
	private static T instance = null;
	private static bool singletonInitialized = false;
	
	public static T Instance {
		get {
			if (singletonInitialized == false)
			{
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType<T>() as T;
				}

				if (instance != null)
				{
					singletonInitialized = true;
				}
			}

			return instance;
		}
		private set {
			instance = value;
		}
	}

	protected virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;

			singletonInitialized = true;
		}
		else if (Instance != this)
		{
			Debug.LogError("Multiple instances of " + this + " found. Destroying duplicate instance.");
			
			Destroy(gameObject);
		}
	}
}
