using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	[SerializeField] List<GameObject> cardPrefabs = new List<GameObject>();
	[SerializeField] List<GameObject> spawnedCards = new List<GameObject>();
	[SerializeField] HashSet<int> selectedIndices = new HashSet<int>();

	[Header("Components")]
	[SerializeField] Transform cardParent;
	public Transform CardParent { get { return cardParent; } }

	[Header("Vector")]
	[SerializeField] Vector2[] anchorPositions;

	private void Start()
	{
		cardParent = GameObject.FindGameObjectWithTag("CardImage").transform;
		cardParent.gameObject.SetActive(false);
	}

	private void SpawnRandomCards()
	{
		foreach (GameObject card in spawnedCards)
		{
			Destroy(card);
		}

		spawnedCards.Clear();
		selectedIndices.Clear();

		while (selectedIndices.Count < 3)
		{
			int randomIndex = Random.Range(0, cardPrefabs.Count);
			selectedIndices.Add(randomIndex);
		}

		int i = 0;

		foreach (int index in selectedIndices)
		{
			GameObject cardObj = Instantiate(cardPrefabs[index], cardParent);
			RectTransform cardTransform = cardObj.GetComponent<RectTransform>();

			cardTransform.sizeDelta = new Vector2(300, 450);
			cardTransform.anchorMin = anchorPositions[i];
			cardTransform.anchorMax = anchorPositions[i];
			cardTransform.anchoredPosition = Vector2.zero;
			cardTransform.rotation = Quaternion.identity;

			spawnedCards.Add(cardObj);
			i++;
		}
	}

	public void FlipAllCards()
	{
		cardParent.gameObject.SetActive(true);

		SpawnRandomCards();

		foreach (GameObject cardObj in spawnedCards)
		{
			Card card = cardObj.GetComponent<Card>();
			if (card != null)
			{
				card.FlipCard();
			}
		}
	}
}
