using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadSpawner : MonoBehaviour
{
	private HashSet<Vector2Int> placedTiles = new HashSet<Vector2Int>();
	private HashSet<Vector2Int> blockedTiles = new HashSet<Vector2Int>();
	private List<Vector2Int> firstRoadPath = new List<Vector2Int>();

	[Header("Components")]
	[SerializeField] Tilemap tilemap;
	[SerializeField] TileBase roadTile;

	public void GenerateRoads(List<Vector2Int> castlePositions)
	{
		Vector2Int start = castlePositions[0];
		Vector2Int end = castlePositions[1];

		GenerateCurvyRoad(start, end, isFirstRoad: true);

		Vector2Int newStart = FindNewStartPosition();
		GenerateCurvyRoad(newStart, end, isFirstRoad: false);
	}

	private void GenerateCurvyRoad(Vector2Int start, Vector2Int end, bool isFirstRoad)
	{
		Vector2Int current = start;
		PlaceTile(current, isFirstRoad);

		while (current != end)
		{
			List<Vector2Int> possibleDirections = GetPossibleDirections(current, end, isFirstRoad);

			Vector2Int nextTile = possibleDirections[Random.Range(0, possibleDirections.Count)];
			current = nextTile;
			PlaceTile(current, isFirstRoad);
		}

		PlaceTile(end, isFirstRoad);
		tilemap.RefreshAllTiles();
	}

	private void PlaceTile(Vector2Int position, bool isFirstRoad)
	{
		tilemap.SetTile((Vector3Int)position, roadTile);
		placedTiles.Add(position);

		if (isFirstRoad)
		{
			firstRoadPath.Add(position);
			MarkBlockedTiles(position);
		}
	}

	private void MarkBlockedTiles(Vector2Int position)
	{
		Vector2Int[] neighbors = new Vector2Int[]
		{
			new Vector2Int(0, 1),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0),
			new Vector2Int(1, 0)
		};

		foreach (Vector2Int neighbor in neighbors)
		{
			blockedTiles.Add(position + neighbor);
		}
	}

	private List<Vector2Int> GetPossibleDirections(Vector2Int current, Vector2Int end, bool isFirstRoad)
	{
		List<Vector2Int> directions = new List<Vector2Int>();

		Vector2Int[] possibleMoves = new Vector2Int[]
		{
			new Vector2Int(0, 1),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0),
			new Vector2Int(1, 0)
		};

		foreach (Vector2Int move in possibleMoves)
		{
			Vector2Int newPosition = current + move;

			if (isFirstRoad)
			{
				if (!placedTiles.Contains(newPosition))
				{
					if (IsCloserToTarget(newPosition, end, current))
					{
						directions.Add(newPosition);
					}
				}
			}
			else
			{
				if (!placedTiles.Contains(newPosition) && !blockedTiles.Contains(newPosition))
				{
					if (IsCloserToTarget(newPosition, end, current))
					{
						directions.Add(newPosition);
					}
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

	private Vector2Int FindNewStartPosition()
	{
		foreach (Vector2Int tile in firstRoadPath)
		{
			Vector2Int[] possibleStarts = new Vector2Int[]
			{
				tile + new Vector2Int(2, 0),
				tile + new Vector2Int(-2, 0),
				tile + new Vector2Int(0, 2),
				tile + new Vector2Int(0, -2)
			};

			foreach (Vector2Int newStart in possibleStarts)
			{
				if (!placedTiles.Contains(newStart) && !blockedTiles.Contains(newStart))
				{
					return newStart;
				}
			}
		}
		return Vector2Int.zero;
	}
}
