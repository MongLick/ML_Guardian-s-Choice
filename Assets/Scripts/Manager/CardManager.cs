using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	private List<PooledObject> pooledObjects = new List<PooledObject>();
	private HashSet<int> selectedIndices = new HashSet<int>();

	[Header("Components")]
	[SerializeField] PooledObject[] cardPrefabs;
	[SerializeField] Transform cardParent;
	public Transform CardParent { get { return cardParent; } }

	[Header("Vector")]
	[SerializeField] Vector2[] anchorPositions;

	private void Start()
	{
		cardParent = GameObject.FindGameObjectWithTag("CardImage").transform;
		cardParent.gameObject.SetActive(false);

		for (int i = 0; i < cardPrefabs.Length; i++)
		{
			Manager.Pool.CreatePool(cardPrefabs[i], 1, 2);

			PooledObject cardObj = Manager.Pool.GetPool(cardPrefabs[i], Vector3.zero, Quaternion.identity);
			cardObj.transform.SetParent(cardParent, false);
			cardObj.Pool.ReturnPool(cardObj);
		}

		Manager.Pool.DestroyPool();
	}

	private void SpawnRandomCards()
	{
		pooledObjects.Clear();
		selectedIndices.Clear();

		int i = 0;

		while (i < 3)
		{
			int randomIndex = Random.Range(0, cardPrefabs.Length);

			while (selectedIndices.Contains(randomIndex))
			{
				randomIndex = Random.Range(0, cardPrefabs.Length);
			}

			selectedIndices.Add(randomIndex);

			PooledObject cardObj = Manager.Pool.GetPool(cardPrefabs[randomIndex], cardParent.transform.position, Quaternion.identity);
			pooledObjects.Add(cardObj);

			Card card = cardObj.GetComponent<Card>();
			card.FlipCard();

			RectTransform cardTransform = cardObj.GetComponent<RectTransform>();
			cardTransform.sizeDelta = new Vector2(300, 450);
			cardTransform.anchorMin = anchorPositions[i];
			cardTransform.anchorMax = anchorPositions[i];
			cardTransform.anchoredPosition = Vector2.zero; 
			cardTransform.rotation = Quaternion.identity;

			i++;
		}
	}

	public void CardChoice()
	{
		foreach(PooledObject pool in pooledObjects)
		{
			pool.Pool.ReturnPool(pool);
		}

		cardParent.gameObject.SetActive(false);
	}

	public void FlipAllCards()
	{
		cardParent.gameObject.SetActive(true);

		SpawnRandomCards();
	}
}
