using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	[Header("Components")]
	[SerializeField] Card[] cards;
	[SerializeField] GameObject cardImage;
	public GameObject CardImage { get { return cardImage; } set { cardImage = value; } }

	private void Start()
	{
		cards = FindObjectsOfType<Card>();
		cardImage = GameObject.FindGameObjectWithTag("CardImage");
		cardImage.gameObject.SetActive(false);
	}

	public void FlipAllCards()
	{
		cardImage.gameObject.SetActive(true);

		foreach (Card card in cards)
		{
			card.FlipCard();
		}
	}
}
