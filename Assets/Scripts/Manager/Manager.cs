using UnityEngine;

public static class Manager
{
	public static TileManager Tile { get { return TileManager.Instance; } }
	public static PoolManager Pool { get { return PoolManager.Instance; } }
	public static MonsterManager Monster { get { return MonsterManager.Instance; } }
	public static GameManager Game { get { return GameManager.Instance; } }
	public static CardManager Card { get { return CardManager.Instance; } }
	public static UIManager UI { get { return UIManager.Instance; } }
	public static SceneManager Scene { get { return SceneManager.Instance; }}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		TileManager.ReleaseInstance();
		TileManager.CreateInstance();

		PoolManager.ReleaseInstance();
		PoolManager.CreateInstance();

		MonsterManager.ReleaseInstance();
		MonsterManager.CreateInstance();

		GameManager.ReleaseInstance();
		GameManager.CreateInstance();

		CardManager.ReleaseInstance();
		CardManager.CreateInstance();

		UIManager.ReleaseInstance();
		UIManager.CreateInstance();

		SceneManager.ReleaseInstance();
		SceneManager.CreateInstance();
	}
}
