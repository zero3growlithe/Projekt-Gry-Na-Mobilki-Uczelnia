using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultGameModeControllers
{
	public class GameplayItemContainer : UIDropTargetElement
	{
		public UnityEvent OnCorrectItem;
		public UnityEvent OnInvalidItem;
		public UnityEvent OnItemDrop;
		public bool CanStoreItem = false;
		public Transform ItemStoreContainer;
		
		public ContainerTypes ContainerType;
		public GameplayItem.ItemTypes[] AcceptedItemTypes;

		public void ClearItemsInContainer ()
		{
			if (ItemStoreContainer == null)
			{
				return;
			}

			for (int i = 0; i < ItemStoreContainer.childCount; i++)
			{
				Destroy(ItemStoreContainer.GetChild(i).gameObject);
			}
		}

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
				OnCorrectItem.Invoke();
			}
			else
			{
				ScoreController.Instance.ReportScore(-100);
				GameModeManager.Instance.GetCurrentGameMode<DefaultGameMode>().AddLives(-1);
				OnInvalidItem.Invoke();
			}

			if (CanStoreItem == false)
			{
				Destroy(target);
			}
			else
			{
				target.transform.SetParent(ItemStoreContainer, true);
				target.transform.position = ItemStoreContainer.position;
			}

			OnItemDrop.Invoke();
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
