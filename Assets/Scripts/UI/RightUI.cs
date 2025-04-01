using UnityEngine;
using UnityEngine.UI;

public class RightUI : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Button gameStartButton;
	[SerializeField] Button cardDrawButton;


	[Header("Specs")]
	[SerializeField] bool isAllSlot;

	private void Start()
	{
		Manager.Game.OnRoundClear += EnableGameStartButton;
		gameStartButton.onClick.AddListener(GameStart);
		cardDrawButton.onClick.AddListener(SpawnRandomCards);
	}

	public void GameStart()
	{
		Manager.Game.GameStart();
		gameStartButton.interactable = false;
	}

	public void SpawnRandomCards()
	{
		isAllSlot = true;

		if (Manager.Game.Gold < 100)
		{
			return;
		}

		foreach(Transform slot in Manager.UI.CardUI.CardPos)
		{
			if (slot.GetComponentInChildren<Card>() == null)
			{
				isAllSlot = false;
				break;
			}
		}

		if (isAllSlot)
		{
			return;
		}

		Manager.Card.SpawnRandomCards();
		Manager.Game.Gold -= 100;
	}

	private void EnableGameStartButton()
	{
		gameStartButton.interactable = true;
	}
}
