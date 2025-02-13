using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
	[Header("Components")]
	[SerializeField] PooledObject monsterPrefab;
	public PooledObject MonsterPrefab { get { return monsterPrefab; } }
	[SerializeField] Color[] monsterColors;
	public Color[] MonsterColors { get { return monsterColors; } }

	[Header("Specs")]
	[SerializeField] float delay;
	public float Delay { get { return delay; } }
	[SerializeField] int monsterCount;
	public int MonsterCount { get { return monsterCount; } set { monsterCount = value; } }
	[SerializeField] int monsterMaxCount;
	public int MonsterMaxCount { get { return monsterMaxCount; } set { monsterMaxCount = value; } }

	public void MonsterSpawn()
	{
		StartCoroutine(MonsterSpawnCoroutine());
	}

	private IEnumerator MonsterSpawnCoroutine()
	{
		Manager.Tile.PathDraw.ClearPath();

		while (monsterCount > 0)
		{
			Manager.Pool.GetPool(Manager.Monster.MonsterPrefab, Manager.Tile.StartPos, Quaternion.identity);
			monsterCount--;
			yield return new WaitForSeconds(delay);
		}

		monsterCount = monsterMaxCount;
	}
}
