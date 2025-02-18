using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	private List<Node> path;

	[Header("Components")]
	[SerializeField] PathFinding pathFinding;
	[SerializeField] Animator animator;
	[SerializeField] PooledObject pooledObject;
	[SerializeField] SpriteRenderer spriteRenderer;

	[Header("Specs")]
	[SerializeField] float health;
	[SerializeField] float moveSpeed;
	[SerializeField] float stopDist;
	[SerializeField] int currentNodeIndex;
	[SerializeField] bool isMoving;

	private void OnEnable()
	{
		spriteRenderer.color = Manager.Monster.MonsterColors[Manager.Game.GameRound];
		health = health * Manager.Game.GameRound * 2;
	}

	private void Start()
	{
		pathFinding = Manager.Tile.PathFinding;

		path = pathFinding.FinalNodeList;

		if (path != null && path.Count > 0)
		{
			isMoving = true;
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
			currentNodeIndex = 1;
			pooledObject.Pool.ReturnPool(pooledObject);
			Manager.Monster.MonsterDeadCount--;

			if(Manager.Monster.MonsterDeadCount == 0)
			{
				Manager.Game.IsStageStart = false;
				Manager.Game.OnStageClear?.Invoke();
			}
		}
	}
}
