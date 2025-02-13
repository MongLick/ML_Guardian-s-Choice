using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[Header("Specs")]
	[SerializeField] int gameRound;
	public int GameRound { get { return gameRound; } }
	[SerializeField] bool isRoundActive;
	public bool IsRoundActive { get { return isRoundActive; } set { isRoundActive = value; } }

	public void GameStart()
	{
		gameRound++;
		Manager.Monster.MonsterSpawn();
	}
}
