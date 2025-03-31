using UnityEngine;

public class GameScene : BaseScene
{
	[Header("Components")]
	[SerializeField] StartEndTileSpawner startEndTileSpawner;

	private void Start()
	{
		Manager.Tile.GameStart();
		Manager.Card.GameStart();
		Manager.Pool.CreatePool(Manager.Monster.MonsterPrefab, 15, 30);
		Manager.Tile.PathWait();
		startEndTileSpawner.StartEndTileSpawn();
	}

	public void GameStart()
	{
		Manager.Game.GameStart();
	}

	public void SpawnRandomCards()
	{
		Manager.Card.SpawnRandomCards();
	}
}
