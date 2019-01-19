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
		public GameplayItem.ItemTypes[] AcceptedItemTypes;

		public GameplayItemContainer(ContainerTypes type)
		{
			this.ContainerType = type;
		}

		public bool ItemMatchesContainter(GameplayItem item)
		{
			for (int i = 0; i < AcceptedItemTypes.Length; i++)
			{
				if (item.ItemType == AcceptedItemTypes[i])
				{
					return true;
				}
			}

			return false;
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

			Destroy(target);
		}

		public enum ContainerTypes
		{
			CAT,
			POT,
			HEDGEHOG,
			TRASH,
			FLOOR,
		}
	}
}
