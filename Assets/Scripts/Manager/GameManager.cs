using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[Header("Components")]
	[SerializeField] PathFinding pathFinding;
	public PathFinding PathFinding { get { return pathFinding; } set { pathFinding = value; } }
	[SerializeField] PathDraw pathDraw;
	public PathDraw PathDraw { get { return pathDraw; } set { pathDraw = value; } }

	[Header("Specs")]
	[SerializeField] int gameRound;
	public int GameRound { get { return gameRound; } }

	public void GameStart()
	{
		pathFinding.Finding();
		pathDraw.Drawing();
		StartCoroutine(MonsterSpawn());
	}

	private IEnumerator MonsterSpawn()
	{
		while (gameRound < 11)
		{
			Manager.Pool.GetPool(Manager.Monster.MonsterPrefab, Manager.Spawn.StartPos, Quaternion.identity);
			gameRound++;
			yield return new WaitForSeconds(2f);
		}
	}
}
