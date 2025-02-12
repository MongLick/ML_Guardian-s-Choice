using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	private List<Node> finalNodeList;
	public List<Node> FinalNodeList { get { return finalNodeList; } }
	private List<Node> openList;
	private List<Node> closedList;

	[Header("Components")]
	[SerializeField] Node[,] nodeArray;
	[SerializeField] Node startNode;
	[SerializeField] Node targetNode;
	[SerializeField] Node curNode;

	[Header("Vector")]
	[SerializeField] Vector2Int bottomLeft;
	[SerializeField] Vector2Int topRight;
	[SerializeField] Vector2Int startPos;
	[SerializeField] Vector2Int targetPos;

	[Header("Specs")]
	[SerializeField] int sizeX;
	[SerializeField] int sizeY;

	private void Start()
	{
		Manager.Game.PathFinding = this;
	}

	public void Finding()
	{
		finalNodeList = new List<Node>();

		Vector3Int startTilePos = Manager.Spawn.StartPos;
		Vector3Int endTilePos = Manager.Spawn.EndPos;

		startPos = new Vector2Int(startTilePos.x, startTilePos.y);
		targetPos = new Vector2Int(endTilePos.x, endTilePos.y);

		sizeX = topRight.x - bottomLeft.x + 1;
		sizeY = topRight.y - bottomLeft.y + 2;
		nodeArray = new Node[sizeX, sizeY];

		for (int i = 0; i < sizeX; i++)
		{
			for (int j = 0; j < sizeY; j++)
			{
				bool isWall = false;
				foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
					if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

				nodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
			}
		}

		startNode = nodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
		targetNode = nodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

		openList = new List<Node>() { startNode };
		closedList = new List<Node>();

		while (openList.Count > 0)
		{
			curNode = openList[0];
			for (int i = 1; i < openList.Count; i++)
				if (openList[i].F <= curNode.F && openList[i].H < curNode.H) curNode = openList[i];

			openList.Remove(curNode);
			closedList.Add(curNode);

			if (curNode == targetNode)
			{
				Node TargetCurNode = targetNode;
				while (TargetCurNode != startNode)
				{
					FinalNodeList.Add(TargetCurNode);
					TargetCurNode = TargetCurNode.ParentNode;
				}
				FinalNodeList.Add(startNode);
				FinalNodeList.Reverse();
			}

			OpenListAdd(curNode.X, curNode.Y + 1);
			OpenListAdd(curNode.X + 1, curNode.Y);
			OpenListAdd(curNode.X, curNode.Y - 1);
			OpenListAdd(curNode.X - 1, curNode.Y);
		}
	}

	private void OpenListAdd(int checkX, int checkY)
	{
		if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].IsWall && !closedList.Contains(nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
		{
			Node NeighborNode = nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
			int MoveCost = curNode.G + 10;

			if (MoveCost < NeighborNode.G || !openList.Contains(NeighborNode))
			{
				NeighborNode.G = MoveCost;
				NeighborNode.H = (Mathf.Abs(NeighborNode.X - targetNode.X) + Mathf.Abs(NeighborNode.Y - targetNode.Y)) * 10;
				NeighborNode.ParentNode = curNode;

				openList.Add(NeighborNode);
			}
		}
	}
}
