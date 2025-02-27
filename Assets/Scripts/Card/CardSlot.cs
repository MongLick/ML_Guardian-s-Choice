using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
	[SerializeField] Card currentCard;
	public Card CurrentCard { get { return currentCard; } set { currentCard = value; } }

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			Card draggedCard = eventData.pointerDrag.GetComponent<Card>();

			if (draggedCard == null)
			{
				return;
			}

			if (currentCard == null)
			{
				PlaceCard(draggedCard);
			}
			else
			{
				SwapCards(draggedCard);
			}
		}
	}

	private void PlaceCard(Card newCard)
	{
		newCard.CardSlot.CurrentCard = null;
		newCard.CardSlot = this;
		newCard.transform.SetParent(transform, false);
		newCard.transform.position = transform.position;
		currentCard = newCard;
	}

	private void SwapCards(Card newCard)
	{
		currentCard.transform.SetParent(newCard.CardSlot.transform, false);
		currentCard.transform.position = newCard.CardSlot.transform.position;
		currentCard.CardSlot = newCard.CardSlot;
		newCard.CardSlot.CurrentCard = currentCard;

		newCard.transform.SetParent(this.transform, false);
		newCard.transform.position = this.transform.position;
		newCard.CardSlot = this;
		currentCard = newCard;
	}
}
