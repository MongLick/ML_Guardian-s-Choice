using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	private List<Node> path;

	[Header("Components")]
	[SerializeField] PathFinding pathFinding;

	[Header("Specs")]
	[SerializeField] float moveSpeed;
	[SerializeField] float stopDist;
	[SerializeField] int currentNodeIndex;
	[SerializeField] bool isMoving;

	public void GameStart()
	{
		gameObject.transform.position = new Vector3(pathFinding.FinalNodeList[0].X, pathFinding.FinalNodeList[0].Y, 0);

		path = pathFinding.FinalNodeList;

		if (path != null && path.Count > 0)
		{
			isMoving = true;
			MoveToNextNode();
		}
	}

	private void Update()
	{
		if (isMoving && path != null && path.Count > 0)
		{
			MoveAlongPath();
		}
	}

	private void MoveAlongPath()
	{
		if (currentNodeIndex < path.Count)

		{
			Vector3 targetPosition = new Vector3(path[currentNodeIndex].X, path[currentNodeIndex].Y, 0);

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, targetPosition) < stopDist)
			{
				currentNodeIndex++;
			}
		}

		else
		{
			isMoving = false; 
		}
	}

	private void MoveToNextNode()
	{
		currentNodeIndex = 0;
	}
}
