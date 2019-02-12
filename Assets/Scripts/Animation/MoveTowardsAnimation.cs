using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTowardsAnimation : MonoBehaviour
{
	public Transform targetToMove;
	public Transform startPoint;
	public Transform endPoint;
	public float speed = 1;
    public UnityEvent OnReachStartPoint;
    public UnityEvent OnReachEndPoint;

	public void GoTowardsStartPoint ()
	{
		StopAllCoroutines();
		StartCoroutine(MoveTowardsPoint(startPoint, OnReachStartPoint));
	}

	public void GoTowardsEndPoint ()
	{
		StopAllCoroutines();
		StartCoroutine(MoveTowardsPoint(endPoint, OnReachEndPoint));
	}

	private IEnumerator MoveTowardsPoint (Transform targetPoint, UnityEvent eventToInvoke)
	{
		float distanceToTarget = Vector3.Distance(targetToMove.position, targetPoint.position);

		while (Mathf.Approximately(distanceToTarget, 0) == false)
		{
			targetToMove.position = Vector3.MoveTowards(targetToMove.position, targetPoint.position, Time.deltaTime * speed);

			distanceToTarget = Vector3.Distance(targetToMove.position, targetPoint.position);

			yield return null;
		}

		targetToMove.position = targetPoint.position;

		eventToInvoke.Invoke();
	}
}
