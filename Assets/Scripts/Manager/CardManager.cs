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
	[SerializeField] Transform cardDrawUI;
	public Transform CardDrawUI { get { return cardDrawUI; } }

	[Header("Vector")]
	[SerializeField] Vector2[] anchorPositions;

	private void Start()
	{
		cardParent = GameObject.FindGameObjectWithTag("Canvas").transform;
		cardDrawUI = GameObject.FindGameObjectWithTag("CardImage").transform;

		cardDrawUI.gameObject.SetActive(false);

		for (int i = 0; i < cardPrefabs.Length; i++)
		{
			Manager.Pool.CardCreatePool(cardPrefabs[i], 2, 5);
		}
	}

	public void SpawnRandomCards()
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

			PooledObject cardObj = Manager.Pool.GetPool(cardPrefabs[randomIndex], cardDrawUI.transform.position, Quaternion.identity);
			pooledObjects.Add(cardObj);

			Card card = cardObj.GetComponent<Card>();
			card.FlipCard();
			card.IsDraw = true;

			RectTransform cardTransform = cardObj.GetComponent<RectTransform>();
			cardTransform.sizeDelta = new Vector2(300, 450);
			cardTransform.anchorMin = anchorPositions[i];
			cardTransform.anchorMax = anchorPositions[i];
			cardTransform.anchoredPosition = Vector2.zero;
			cardTransform.rotation = Quaternion.identity;

			i++;
		}
	}

	public void CardChoice(int index, int uiIndex)
	{
		cardDrawUI.gameObject.SetActive(false);

		foreach (PooledObject pool in pooledObjects)
		{
			pool.Pool.ReturnPool(pool);
		}

		PooledObject poolObject = Manager.Pool.GetPool(cardPrefabs[index], cardDrawUI.transform.position, Quaternion.identity);
		Card cardObject = poolObject.GetComponent<Card>();
		cardObject.MoveAndScaleToTarget(Manager.UI.CardUI.CardPos[uiIndex].GetComponent<RectTransform>());
		Manager.UI.CardUI.CardPos[uiIndex].GetComponent<CardSlot>().CurrentCard = cardObject;
		cardObject.CardSlot = Manager.UI.CardUI.CardPos[uiIndex].GetComponent<CardSlot>();
	}
}
