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
}
