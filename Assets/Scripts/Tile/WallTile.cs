using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Tilemap tileMap;

	[Header("Specs")]
	[SerializeField] bool isHighlight;

	public void Highlight(bool show)
	{
		isHighlight = show;
		tileMap.color = show ? Color.green : Color.white;
	}
}
