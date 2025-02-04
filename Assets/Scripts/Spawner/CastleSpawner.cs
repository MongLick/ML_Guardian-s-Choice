using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CastleSpawner : MonoBehaviour
{
	private List<Vector2Int> castlePositions = new List<Vector2Int>();

	[Header("Components")]
	[SerializeField] Tilemap tilemap;
	[SerializeField] TileBase[] castleTile;
	[SerializeField] RoadSpawner roadSpawner;

	[Header("Specs")]
	[SerializeField] int castlesNumber;
	[SerializeField] int randomXMin;
	[SerializeField] int randomXMax;
	[SerializeField] int randomYMin;
	[SerializeField] int randomYMax;

	private void Start()
	{
		PlaceCastles();
		roadSpawner.GenerateRoads(castlePositions);
	}

	private void PlaceCastles()
	{
		for (int i = 0; i < castlesNumber; i++)
		{
			Vector2Int newCastlePos = GenerateCastlePosition(i);
			castlePositions.Add(newCastlePos);
			tilemap.SetTile((Vector3Int)newCastlePos, castleTile[i]);
		}
	}

	private Vector2Int GenerateCastlePosition(int castleIndex)
	{
		if (castleIndex == 0)
		{
			return new Vector2Int(Random.Range(-randomXMin, -randomXMax), Random.Range(randomYMin, randomYMax));
		}
		else
		{
			return new Vector2Int(Random.Range(randomXMin, randomXMax), Random.Range(-randomYMin, -randomYMax));
		}
	}
}
