using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearCurve
{
	#region MEMBERS

	[SerializeField]
	private Vector2 timeRange = Vector2.zero;
	[SerializeField]
	private Vector2 valueRange = Vector2.zero;

	// INTERNAL
	private float startValueMultiplier = 1f;
	private float endValueMultiplier = 1f;

	#endregion

	#region PROPERTIES

	public Vector2 TimeRange {
		get {return timeRange;}
		set {timeRange = value;}
	}
	public Vector2 ValueRange {
		get {return valueRange;}
		set {valueRange = value;}
	}

	public float StartTime {
		get {return TimeRange.x;}
		set {TimeRange = TimeRange.SetX(value);}
	}
	public float StartValue {
		get {return ValueRange.x;}
		set {ValueRange = ValueRange.SetX(value);}
	}

	public float EndTime {
		get {return TimeRange.y;}
		set {TimeRange = TimeRange.SetY(value);}
	}
	public float EndValue {
		get {return ValueRange.y;}
		set {ValueRange = ValueRange.SetY(value);}
	}

	public float StartValueMultiplier {
		get {return startValueMultiplier;}
		set {startValueMultiplier = value;}
	}
	public float EndValueMultiplier {
		get {return endValueMultiplier;}
		set {endValueMultiplier = value;}
	}

	#endregion

	#region FUNCTIONS

	public LinearCurve ()
	{
		StartTime = 0;
		StartValue = 0;
		EndTime = 1;
		EndValue = 1;
	}

	public LinearCurve (float xTime, float xValue, float yTime, float yValue)
	{
		StartTime = xTime;
		StartValue = xValue;
		EndTime = yTime;
		EndValue = yValue;
	}

	public float Evaluate (float time)
	{
		float timeLength = EndTime - StartTime;
		float timePercentage = ((time - StartTime) / timeLength);

		float newStartValue = StartValue * StartValueMultiplier;
		float newEndValue = EndValue * EndValueMultiplier;

		float valueLength = newEndValue - newStartValue;
		float output = newStartValue + valueLength * timePercentage;

		if (newStartValue > newEndValue)
		{
			return Mathf.Clamp(output, newEndValue, newStartValue);
		}

		return Mathf.Clamp(output, newStartValue, newEndValue);
	}

	#endregion
}
