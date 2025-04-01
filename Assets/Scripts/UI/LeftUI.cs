using TMPro;
using UnityEngine;

public class LeftUI : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] TMP_Text roundText;
	[SerializeField] TMP_Text healthText;
	[SerializeField] TMP_Text goldText;

	private void Start()
	{
		Manager.Game.OnGameRound += UpdateRoundUI;
		Manager.Game.OnHealthChanged += UpdateHealthUI;
		Manager.Game.OnGoldChanged += UpdateGoldUI;
	}

	private void UpdateRoundUI(int round)
	{
		roundText.text = $"�������� : {round} / 30";
	}

	private void UpdateHealthUI(int health)
	{
		healthText.text = $"ü�� : {health} / 25 ";
	}

	private void UpdateGoldUI(int gold)
	{
		goldText.text = $"��� : {gold}";
	}
}
