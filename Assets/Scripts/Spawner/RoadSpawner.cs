using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadSpawner : MonoBehaviour
{
	private HashSet<Vector2Int> placedTiles = new HashSet<Vector2Int>();

	[Header("Components")]
	[SerializeField] Tilemap tilemap;
	[SerializeField] TileBase roadTile;

	public void GenerateRoads(List<Vector2Int> castlePositions)
	{
		Vector2Int start = castlePositions[0];
		Vector2Int end = castlePositions[1];

		GenerateCurvyRoad(start, end);
	}

	private void GenerateCurvyRoad(Vector2Int start, Vector2Int end)
	{
		Vector2Int current = start;

		tilemap.SetTile((Vector3Int)current, roadTile);
		placedTiles.Add(current);

		while (current != end)
		{
			List<Vector2Int> possibleDirections = GetPossibleDirections(current, end);

			Vector2Int nextTile = possibleDirections[Random.Range(0, possibleDirections.Count)];

			current = nextTile;

			tilemap.SetTile((Vector3Int)current, roadTile);
			placedTiles.Add(current);
		}

		tilemap.SetTile((Vector3Int)end, roadTile);
		placedTiles.Add(end);

		tilemap.RefreshAllTiles();
	}

	private List<Vector2Int> GetPossibleDirections(Vector2Int current, Vector2Int end)
	{
		List<Vector2Int> directions = new List<Vector2Int>();

		Vector2Int[] possibleMoves = new Vector2Int[] {
			new Vector2Int(0, 1),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0),
			new Vector2Int(1, 0)
		};

		foreach (Vector2Int move in possibleMoves)
		{
			Vector2Int newPosition = current + move;

			if (!placedTiles.Contains(newPosition))
			{
				if (IsCloserToTarget(newPosition, end, current))
				{
					directions.Add(newPosition);
				}
			}
		}

		return directions;
	}

	private bool IsCloserToTarget(Vector2Int newPosition, Vector2Int end, Vector2Int current)
	{
		int currentDistance = Mathf.Abs(current.x - end.x) + Mathf.Abs(current.y - end.y);
		int newDistance = Mathf.Abs(newPosition.x - end.x) + Mathf.Abs(newPosition.y - end.y);

		return newDistance < currentDistance;
	}
}
