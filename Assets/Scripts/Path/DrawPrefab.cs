using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPrefab : MonoBehaviour
{
	private List<Node> path;

	[Header("Components")]
	[SerializeField] PooledObject pooledObject;
	[SerializeField] PathFinding pathFinding;
	[SerializeField] TrailRenderer trailRenderer;

	[Header("Specs")]
	[SerializeField] float moveSpeed;
	[SerializeField] float stopDist;
	[SerializeField] int currentNodeIndex;
	[SerializeField] bool isMove;
	public bool IsMove { get { return isMove; } }

	public void MoveAlongPath()
	{
		pathFinding = Manager.Tile.PathFinding;

		StartCoroutine(MoveCoroutine());
	}

	private void MoveToNextNode()
	{
		Vector3 targetPosition = new Vector3(path[currentNodeIndex].X, path[currentNodeIndex].Y, 0);

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(transform.position, targetPosition) < stopDist)
		{
			currentNodeIndex++;
		}
	}

	private void ResetMovement()
	{
		currentNodeIndex = 0;
		trailRenderer.Clear();
		pooledObject.Pool.ReturnPool(pooledObject);
		isMove = false;
	}

	private IEnumerator MoveCoroutine()
	{
		isMove = true;

		while (!Manager.Game.IsStageStart)
		{
			path = pathFinding.FinalNodeList;

			if (currentNodeIndex < path.Count)
			{
				MoveToNextNode();
			}
			else
			{
				ResetMovement();
			}

			yield return null;
		}

		ResetMovement();
	}
}
