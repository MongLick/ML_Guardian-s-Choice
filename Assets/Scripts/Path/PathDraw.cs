using System.Collections;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] PathFinding pathFinding;
	[SerializeField] PooledObject pooledObject;
	[SerializeField] DrawPrefab drawPrefab;

	private void Start()
	{
		Manager.Tile.PathDraw = this;
		Manager.Tile.IsDraw = true;
	}

	public void Drawing()
	{
		if (drawPrefab == null)
		{
			drawPrefab = Manager.Tile.DrawPrefab;
			pooledObject = drawPrefab.GetComponent<PooledObject>();
			Manager.Pool.CreatePool(pooledObject, 1, 2);
		}

		StartCoroutine(SpawnPrefab());
	}

	private IEnumerator SpawnPrefab()
	{
		while(!Manager.Game.IsStageStart)
		{
			Vector3 spawnPosition = new Vector3(pathFinding.FinalNodeList[0].X, pathFinding.FinalNodeList[0].Y, 0);
			PooledObject drawObject = Manager.Pool.GetPool(pooledObject, spawnPosition, Quaternion.identity);
			yield return null;
			drawPrefab = drawObject.GetComponent<DrawPrefab>();
			drawPrefab.MoveAlongPath();

			while (drawPrefab.IsMove)
			{
				yield return null;
			}
		}
	}
}
