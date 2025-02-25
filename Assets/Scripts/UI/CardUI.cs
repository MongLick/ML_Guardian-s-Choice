using UnityEngine;

public class CardUI : MonoBehaviour
{
	[SerializeField] Transform[] cardPos;
	public Transform[] CardPos { get { return cardPos; } set { cardPos = value; } }
	[SerializeField] Card[] cards;
	public Card[] Cards { get { return cards; } set { cards = value; } }

	private void Start()
	{
		Manager.UI.CardUI = this;
	}
}