using System.Collections.Generic;
using UnityEngine;

public class WallTileSpawner : MonoBehaviour
{
	[Header("Specs")]
	[SerializeField] int minTiles;
	[SerializeField] int maxTiles;
	[SerializeField] int xPosMin;
	[SerializeField] int xPosMax;
	[SerializeField] int yPosMin;
	[SerializeField] int yPosMax;

	private void OnEnable()
	{
		Manager.Tile.OnWayTileSpawn += PlaceRandomTiles;
	}

	private void OnDisable()
	{
		Manager.Tile.OnWayTileSpawn -= PlaceRandomTiles;
	}

	private void PlaceRandomTiles()
	{
		int numTilesToPlace = Random.Range(minTiles, maxTiles);
		List<Vector2Int> availablePositions = GetAvailablePositions();

		for (int i = 0; i < numTilesToPlace; i++)
		{
			if (availablePositions.Count == 0)
			{
				break;
			}

			PlaceTileAtRandomPosition(availablePositions);
		}
	}

	private List<Vector2Int> GetAvailablePositions()
	{
		List<Vector2Int> availablePositions = new List<Vector2Int>();

		for (int x = -xPosMin; x <= xPosMax; x++)
		{
			for (int y = -yPosMin; y <= yPosMax; y++)
			{
				Vector2Int currentPos = new Vector2Int(x, y);

				if (currentPos != new Vector2Int(Manager.Tile.StartPos.x, Manager.Tile.StartPos.y) &&
					currentPos != new Vector2Int(Manager.Tile.EndPos.x, Manager.Tile.EndPos.y) &&
					!Manager.Tile.WayPlacedTiles.Contains(currentPos))
				{
					availablePositions.Add(currentPos);
				}
			}
		}

		return availablePositions;
	}

	private void PlaceTileAtRandomPosition(List<Vector2Int> availablePositions)
	{
		Vector2Int randomPos = availablePositions[Random.Range(0, availablePositions.Count)];
		Vector3Int tilePosition = new Vector3Int(randomPos.x, randomPos.y, Manager.Tile.StartPos.z);

		Manager.Tile.WallTilemap.SetTile(tilePosition, Manager.Tile.WallTile);
		Manager.Tile.WayPlacedTiles.Add(randomPos);
		availablePositions.Remove(randomPos);
	}
}
