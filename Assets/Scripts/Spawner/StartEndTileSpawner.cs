using UnityEngine;

public class StartEndTileSpawner : MonoBehaviour
{
	[Header("Specs")]
	[SerializeField] int startXPos;
	[SerializeField] int endXPos;
	[SerializeField] int tileZPos;
	[SerializeField] int randomMin;
	[SerializeField] int randomMax;

	public void StartEndTileSpawn()
	{
		int startY = Random.Range(randomMin, randomMax);
		Manager.Tile.StartPos = new Vector3Int(startXPos, startY, tileZPos);
		Manager.Tile.FloorTilemap.SetTile(Manager.Tile.StartPos, Manager.Tile.StartTile);
		Instantiate(Manager.Tile.StartParticle, Manager.Tile.StartPos, Quaternion.identity); 

		int endY = Random.Range(randomMin, randomMax);
		Manager.Tile.EndPos = new Vector3Int(endXPos, endY, tileZPos);
		Manager.Tile.FloorTilemap.SetTile(Manager.Tile.EndPos, Manager.Tile.EndTile);
		Instantiate(Manager.Tile.EndParticle, Manager.Tile.EndPos, Quaternion.identity);

		Manager.Tile.OnStartEndTileSpawn?.Invoke();
	}
}
