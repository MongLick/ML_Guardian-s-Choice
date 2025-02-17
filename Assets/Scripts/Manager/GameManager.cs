using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	[Header("UnityAction")]
	private UnityAction onStageClear;
	public UnityAction OnStageClear { get { return onStageClear; } set { onStageClear = value; } }

	[Header("Specs")]
	[SerializeField] int gameRound;
	public int GameRound { get { return gameRound; } }

	public void GameStart()
	{
		gameRound++;
		Manager.Monster.MonsterSpawn();
	}
}
