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
	[SerializeField] bool isStageStart;
	public bool IsStageStart { get { return isStageStart; } set { isStageStart = value; } }

	public void GameStart()
	{
		isStageStart = true;
		gameRound++;
		Manager.Monster.MonsterSpawn();
	}
}
