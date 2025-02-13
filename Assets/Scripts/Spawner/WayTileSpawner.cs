using System.Collections.Generic;
using UnityEngine;

public class WayTileSpawner : MonoBehaviour
{
	[Header("Vector")]
	[SerializeField] Vector3Int[] directions;

	[Header("Specs")]
	[SerializeField] int xPosMin;
	[SerializeField] int xPosMax;
	[SerializeField] int yPosMin;
	[SerializeField] int yPosMax;

	private void OnEnable()
	{
		Manager.Tile.OnStartEndTileSpawn += CreateRandomWayTile;
	}

	private void OnDisable()
	{
		Manager.Tile.OnStartEndTileSpawn -= CreateRandomWayTile;
	}

	private void CreateRandomWayTile()
	{
		List<Vector3Int> path = new List<Vector3Int>();
		Vector3Int currentPos = Manager.Tile.StartPos;

		Manager.Tile.WayPlacedTiles.Add(new Vector2Int(currentPos.x, currentPos.y));

		while (currentPos != Manager.Tile.EndPos)
		{
			if (currentPos != Manager.Tile.StartPos && currentPos != Manager.Tile.EndPos)
			{
				Manager.Tile.FloorTilemap.SetTile(currentPos, Manager.Tile.WayTile);
			}

			path.Add(currentPos);

			if (IsAdjacent(currentPos, Manager.Tile.EndPos))
			{
				currentPos = Manager.Tile.EndPos;
				path.Add(currentPos);
				break;
			}

			List<Vector3Int> possibleMoves = GetPossibleMoves(currentPos);

			currentPos = possibleMoves[Random.Range(0, possibleMoves.Count)];

			Manager.Tile.WayPlacedTiles.Add(new Vector2Int(currentPos.x, currentPos.y));
		}

		path.Add(Manager.Tile.EndPos);

		Manager.Tile.OnWayTileSpawn?.Invoke();
	}

	private List<Vector3Int> GetPossibleMoves(Vector3Int currentPos)
	{
		List<Vector3Int> possibleMoves = new List<Vector3Int>();

		foreach (Vector3Int direction in directions)
		{
			Vector3Int newPos = currentPos + direction;

			if (IsValidMove(newPos, currentPos))
			{
				possibleMoves.Add(newPos);
			}
		}

		return possibleMoves;
	}

	private bool IsValidMove(Vector3Int newPos, Vector3Int currentPos)
	{
		if (newPos.x < -xPosMin || newPos.x > xPosMax || newPos.y < -yPosMin || newPos.y > yPosMax)
		{
			return false;
		}

		if (Manager.Tile.WayPlacedTiles.Contains(new Vector2Int(newPos.x, newPos.y)))
		{
			return false;
		}

		if (currentPos.x == Manager.Tile.EndPos.x - 1)
		{
			if (newPos.x > currentPos.x)
			{
				return true;
			}

			if (Manager.Tile.EndPos.y > currentPos.y && newPos.y < currentPos.y)
			{
				return false;
			}

			if (Manager.Tile.EndPos.y < currentPos.y && newPos.y > currentPos.y)
			{
				return false;
			}
		}

		if (currentPos.x == Manager.Tile.EndPos.x)
		{
			if (Manager.Tile.EndPos.y > currentPos.y && newPos.y < currentPos.y)
			{
				return false;
			}

			if (Manager.Tile.EndPos.y < currentPos.y && newPos.y > currentPos.y)
			{
				return false;
			}
		}

		return true;
	}

	private bool IsAdjacent(Vector3Int a, Vector3Int b)
	{
		return (Mathf.Abs(a.x - b.x) == 1 && a.y == b.y) || (Mathf.Abs(a.y - b.y) == 1 && a.x == b.x);
	}
}
