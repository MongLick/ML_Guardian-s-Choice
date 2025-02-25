using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	[Header("Components")]
	
	[SerializeField] CardUI cardUI;
	public CardUI CardUI { get { return cardUI; } set { cardUI = value; } }
}
