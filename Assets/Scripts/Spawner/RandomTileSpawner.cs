using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTileSpawner : MonoBehaviour
{
	private void OnEnable()
	{
		Manager.Spawn.OnStartEndTileSpawn += CreateRandomPath;
	}

	private void OnDisable()
	{
		Manager.Spawn.OnStartEndTileSpawn -= CreateRandomPath;
	}

	private void CreateRandomPath()
	{
		List<Vector3Int> path = new List<Vector3Int>();
		Vector3Int currentPos = Manager.Spawn.StartPos;

		while (currentPos != Manager.Spawn.EndPos)
		{

			if (currentPos != Manager.Spawn.StartPos && currentPos != Manager.Spawn.EndPos)
			{
				Manager.Spawn.Tilemap.SetTile(currentPos, Manager.Spawn.PathTile);
			}

			path.Add(currentPos);

			List<Vector3Int> possibleMoves = GetPossibleMoves(currentPos);

			Vector3Int move = possibleMoves[Random.Range(0, possibleMoves.Count)];

			currentPos = move; 

			Manager.Spawn.PlacedTiles.Add(new Vector2Int(currentPos.x, currentPos.y));
		}

		path.Add(Manager.Spawn.EndPos);
	}

	private List<Vector3Int> GetPossibleMoves(Vector3Int currentPos)
	{
		List<Vector3Int> possibleMoves = new List<Vector3Int>();

		Vector3Int[] directions = new Vector3Int[]
		{
			new Vector3Int(0, 1, 0), 
            new Vector3Int(0, -1, 0),
            new Vector3Int(1, 0, 0),
        };

		foreach (Vector3Int direction in directions)
		{
			Vector3Int newPos = currentPos + direction;

			if (!Manager.Spawn.PlacedTiles.Contains(new Vector2Int(newPos.x, newPos.y)) && IsValidMove(newPos))
			{
				possibleMoves.Add(newPos);
			}
		}

		return possibleMoves;
	}

	private bool IsValidMove(Vector3Int newPos)
	{
		if (newPos.x < -8 || newPos.x > 7 || newPos.y < -1 || newPos.y > 3)
		{
			return false;
		}

		if (newPos.x < Manager.Spawn.StartPos.x)
		{
			return false;
		}

		if (Manager.Spawn.PlacedTiles.Contains(new Vector2Int(newPos.x, newPos.y)))
		{
			return false;
		}

		return true;
	}
}
