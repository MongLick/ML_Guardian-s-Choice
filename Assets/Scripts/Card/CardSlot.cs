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
			if (draggedCard == null) return;
			if (CurrentCard == null)
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
		newCard.transform.SetParent(transform, false);
		newCard.transform.position = transform.position;
		CurrentCard = newCard;
	}

	private void SwapCards(Card newCard)
	{
		Transform oldSlot = CurrentCard.transform.parent; 

		CurrentCard.transform.SetParent(newCard.transform.parent, false);
		CurrentCard.transform.position = newCard.transform.parent.position;

		newCard.transform.SetParent(oldSlot, false);
		newCard.transform.position = oldSlot.position;

		CurrentCard = newCard;
	}
}
