using UnityEngine;

public static class Manager
{
	public static SpawnManager Spawn { get { return SpawnManager.Instance; } }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		SpawnManager.ReleaseInstance();
		SpawnManager.CreateInstance();
	}
}
