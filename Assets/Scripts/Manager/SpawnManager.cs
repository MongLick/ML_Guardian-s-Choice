using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class SpawnManager : Singleton<SpawnManager>
{
	private HashSet<Vector2Int> placedTiles = new HashSet<Vector2Int>();
	public HashSet<Vector2Int> PlacedTiles { get { return placedTiles; } }

	[Header("UnityAction")]
	private UnityAction onStartEndTileSpawn;
	public UnityAction OnStartEndTileSpawn { get { return onStartEndTileSpawn; } set { onStartEndTileSpawn = value; } }

	[Header("Components")]
	[SerializeField] Tilemap tilemap;
	public Tilemap Tilemap { get { return tilemap; } }
	[SerializeField] TileBase startTile;
	public TileBase StartTile { get { return startTile; } }
	[SerializeField] TileBase endTile;
	public TileBase EndTile { get { return endTile; } }
	[SerializeField] TileBase pathTile;
	public TileBase PathTile { get { return pathTile; } }

	[Header("Vector")]
	[SerializeField] Vector3Int startPos;
	public Vector3Int StartPos { get { return startPos; } set { startPos = value; } }
	[SerializeField] Vector3Int endPos;
	public Vector3Int EndPos { get { return endPos; } set { endPos = value; } }

	private void Start()
	{
		if (tilemap == null)
		{
			tilemap = FindObjectOfType<Tilemap>();
		}
	}
}
