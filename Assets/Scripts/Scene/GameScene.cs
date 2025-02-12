using UnityEngine;

public class GameScene : MonoBehaviour
{
	private void Start()
	{
		Manager.Pool.CreatePool(Manager.Monster.MonsterPrefab, 15, 30);
	}

	public void GameStart()
	{
		Manager.Game.GameStart();
	}
}
