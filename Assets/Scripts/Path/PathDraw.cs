using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
	private List<PooledObject> placedPaths = new List<PooledObject>();

	[Header("Components")]
	[SerializeField] PathFinding pathFinding;
	[SerializeField] PooledObject pathPrefab;

	private void Start()
	{
		Manager.Tile.PathDraw = this;
		Manager.Tile.IsDraw = true;
		Manager.Pool.CreatePool(pathPrefab, 30, 40);
	}

	public void Drawing()
	{
		ClearPath();
		foreach (Node node in pathFinding.FinalNodeList)
		{
			if(node == pathFinding.FinalNodeList[0] || node == pathFinding.FinalNodeList[pathFinding.FinalNodeList.Count - 1])
			{
				continue;
			}
			Vector3 pos = new Vector3(node.X, node.Y, 0);
			PooledObject newPath = Manager.Pool.GetPool(pathPrefab, pos, Quaternion.identity);
			placedPaths.Add(newPath);
		}
	}

	public void ClearPath()
	{
		foreach (PooledObject path in placedPaths)
		{
			path.Pool.ReturnPool(path);
		}
		placedPaths.Clear();
	}
}
