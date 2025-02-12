using UnityEngine;

public static class Manager
{
	public static SpawnManager Spawn { get { return SpawnManager.Instance; } }
	public static PoolManager Pool { get { return PoolManager.Instance; } }
	public static MonsterManager Monster { get { return MonsterManager.Instance; } }
	public static GameManager Game { get { return GameManager.Instance; } }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		SpawnManager.ReleaseInstance();
		SpawnManager.CreateInstance();

		PoolManager.ReleaseInstance();
		PoolManager.CreateInstance();

		MonsterManager.ReleaseInstance();
		MonsterManager.CreateInstance();

		GameManager.ReleaseInstance();
		GameManager.CreateInstance();
	}
}
