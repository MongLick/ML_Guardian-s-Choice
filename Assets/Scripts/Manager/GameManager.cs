using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	[Header("UnityAction")]
	private UnityAction onRoundClear;
	public UnityAction OnRoundClear { get { return onRoundClear; } set { onRoundClear = value; } }
	private UnityAction<int> onGameRound;
	public UnityAction<int> OnGameRound { get { return onGameRound; } set { onGameRound = value; } }
	private UnityAction<int> onHealthChanged;
	public UnityAction<int> OnHealthChanged { get { return onHealthChanged; } set { onHealthChanged = value; } }
	private UnityAction<int> onGoldChanged;
	public UnityAction<int> OnGoldChanged { get { return onGoldChanged; } set { onGoldChanged = value; } }

	[Header("Specs")]
	[SerializeField] int gameRound;
	public int GameRound { get { return gameRound; } set { gameRound = value; onGameRound?.Invoke(gameRound); } }
	[SerializeField] int health;
	public int Health { get { return health; } set { health = value; onHealthChanged?.Invoke(health); } }
	[SerializeField] int gold;
	public int Gold { get { return gold; } set { gold = value; onGoldChanged?.Invoke(gold); } }
	[SerializeField] bool isRoundStart;
	public bool IsRoundStart { get { return isRoundStart; } set { isRoundStart = value; } }

	protected override void Awake()
	{
		GameRound = 0;
		health = 25;
		gold = 800;
		isRoundStart = false;
	}

	public void GameStart()
	{
		if (!isRoundStart)
		{
			isRoundStart = true;
			GameRound++;
			Manager.Monster.MonsterSpawn();
		}
	}
}
