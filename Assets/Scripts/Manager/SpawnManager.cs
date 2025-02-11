using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class SpawnManager : Singleton<SpawnManager>
{
	private HashSet<Vector2Int> wayPlacedTiles = new HashSet<Vector2Int>();
	public HashSet<Vector2Int> WayPlacedTiles { get { return wayPlacedTiles; } }

	[Header("UnityAction")]
	private UnityAction onStartEndTileSpawn;
	public UnityAction OnStartEndTileSpawn { get { return onStartEndTileSpawn; } set { onStartEndTileSpawn = value; } }
	private UnityAction onWayTileSpawn;
	public UnityAction OnWayTileSpawn { get { return onWayTileSpawn; } set { onWayTileSpawn = value; } }

	[Header("Components")]
	[SerializeField] Tilemap floorTilemap;
	public Tilemap FloorTilemap { get { return floorTilemap; } }
	[SerializeField] Tilemap wallTilemap;
	public Tilemap WallTilemap { get { return wallTilemap; } }
	[SerializeField] TileBase startTile;
	public TileBase StartTile { get { return startTile; } }
	[SerializeField] TileBase endTile;
	public TileBase EndTile { get { return endTile; } }
	[SerializeField] TileBase wayTile;
	public TileBase WayTile { get { return wayTile; } }
	[SerializeField] TileBase wallTile;
	public TileBase WallTile { get { return wallTile; } }

	[Header("Vector")]
	[SerializeField] Vector3Int startPos;
	public Vector3Int StartPos { get { return startPos; } set { startPos = value; } }
	[SerializeField] Vector3Int endPos;
	public Vector3Int EndPos { get { return endPos; } set { endPos = value; } }

	private void Start()
	{
		if (floorTilemap == null)
		{
			GameObject floorTile = GameObject.FindWithTag("Floor");
			floorTilemap = floorTile.GetComponent<Tilemap>();
		}
		if (wallTilemap == null)
		{
			GameObject wallTile = GameObject.FindWithTag("Wall");
			wallTilemap = wallTile.GetComponent<Tilemap>();
		}
	}
}
