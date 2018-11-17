using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultGameModeControllers
{
	public class ScoreController : MonoBehaviourSingleton<ScoreController>
	{
		#region MEMBERS

		[SerializeField]
		private int currentScore;

		#endregion

		#region PROPERTIES

		public int CurrentScore {
			get { return currentScore; }
			private set { currentScore = value; }
		}

		#endregion

		#region FUNCTIONS

		public void ReportScore(int value)
		{
			CurrentScore = Mathf.Clamp(CurrentScore + value, 0, int.MaxValue);
		}

		public void ResetScore()
		{
			CurrentScore = 0;
		}

		protected override void Awake()
		{
			base.Awake();


		}

		protected void OnDestroy()
		{

		}

		#endregion

		#region CLASS_ENUMS

		#endregion
	}
}