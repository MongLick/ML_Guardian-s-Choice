using UnityEngine;

public class StartEndTileSpawner : MonoBehaviour
{
	[Header("Specs")]
	[SerializeField] int startXPos;
	[SerializeField] int endXPos;
	[SerializeField] int tileZPos;
	[SerializeField] int randomMin;
	[SerializeField] int randomMax;

	private void Start()
	{
		StartEndTileSpawn();
	}

	private void StartEndTileSpawn()
	{
		int startY = Random.Range(randomMin, randomMax);
		Manager.Spawn.StartPos = new Vector3Int(startXPos, startY, tileZPos);
		Manager.Spawn.Tilemap.SetTile(Manager.Spawn.StartPos, Manager.Spawn.StartTile);

		int endY = Random.Range(randomMin, randomMax);
		Manager.Spawn.EndPos = new Vector3Int(endXPos, endY, tileZPos);
		Manager.Spawn.Tilemap.SetTile(Manager.Spawn.EndPos, Manager.Spawn.EndTile);

		Manager.Spawn.OnStartEndTileSpawn?.Invoke();
	}
}
