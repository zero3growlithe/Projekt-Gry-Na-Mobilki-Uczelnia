using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DefaultGameModeControllers
{
	public class GameplayItemContainer : UIDropTargetElement
	{
		public ContainerTypes ContainerType;

		public GameplayItemContainer(ContainerTypes type)
		{
			this.ContainerType = type;
		}

		public Boolean ItemMatchesContainter(GameplayItem item)
		{
			return ((int)this.ContainerType).Equals((int)item.ItemType % 4);
		}

		protected override void HandleOnDropEvent(GameObject target)
		{
			base.HandleOnDropEvent(target);
			
			// TODO: move to inheriting class
			GameplayItemContainer container = gameObject.GetComponent<GameplayItemContainer>();
			GameplayItem item = target.GetComponent<GameplayItem>();

			if (container.ItemMatchesContainter(item))
			{
				ScoreController.Instance.ReportScore(100);
			}
			else
			{
				ScoreController.Instance.ReportScore(-100);
				GameModeManager.Instance.GetCurrentGameMode<DefaultGameMode>().AddLives(-1);
			}
		}

		public enum ContainerTypes
		{
			CAT = 0,
			POT = 1,
			HEDGEHOG = 2,
			TRASH = 3,
		}
	}
}
