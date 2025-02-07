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
		Manager.Spawn.OnStartEndTileSpawn += CreateRandomWayTile;
	}

	private void OnDisable()
	{
		Manager.Spawn.OnStartEndTileSpawn -= CreateRandomWayTile;
	}

	private void CreateRandomWayTile()
	{
		List<Vector3Int> path = new List<Vector3Int>();
		Vector3Int currentPos = Manager.Spawn.StartPos;

		Manager.Spawn.WayPlacedTiles.Add(new Vector2Int(currentPos.x, currentPos.y));

		while (currentPos != Manager.Spawn.EndPos)
		{
			if (currentPos != Manager.Spawn.StartPos && currentPos != Manager.Spawn.EndPos)
			{
				Manager.Spawn.FloorTilemap.SetTile(currentPos, Manager.Spawn.WayTile);
			}

			path.Add(currentPos);

			if (IsAdjacent(currentPos, Manager.Spawn.EndPos))
			{
				currentPos = Manager.Spawn.EndPos;
				path.Add(currentPos);
				break;
			}

			List<Vector3Int> possibleMoves = GetPossibleMoves(currentPos);

			currentPos = possibleMoves[Random.Range(0, possibleMoves.Count)];

			Manager.Spawn.WayPlacedTiles.Add(new Vector2Int(currentPos.x, currentPos.y));
		}

		path.Add(Manager.Spawn.EndPos);

		Manager.Spawn.OnWayTileSpawn?.Invoke();
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

		if (Manager.Spawn.WayPlacedTiles.Contains(new Vector2Int(newPos.x, newPos.y)))
		{
			return false;
		}

		if (currentPos.x == Manager.Spawn.EndPos.x - 1)
		{
			if (newPos.x > currentPos.x)
			{
				return true;
			}

			if (Manager.Spawn.EndPos.y > currentPos.y && newPos.y < currentPos.y)
			{
				return false;
			}

			if (Manager.Spawn.EndPos.y < currentPos.y && newPos.y > currentPos.y)
			{
				return false;
			}
		}

		if (currentPos.x == Manager.Spawn.EndPos.x)
		{
			if (Manager.Spawn.EndPos.y > currentPos.y && newPos.y < currentPos.y)
			{
				return false;
			}

			if (Manager.Spawn.EndPos.y < currentPos.y && newPos.y > currentPos.y)
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
