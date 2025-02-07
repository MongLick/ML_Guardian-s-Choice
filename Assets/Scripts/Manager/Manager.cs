using UnityEngine;

public static class Manager
{
	public static SpawnManager Spawn { get { return SpawnManager.Instance; } }
	public static GameManager Game { get { return GameManager.Instance; } }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		SpawnManager.ReleaseInstance();
		SpawnManager.CreateInstance();

		GameManager.ReleaseInstance();
		GameManager.CreateInstance();
	}
}
