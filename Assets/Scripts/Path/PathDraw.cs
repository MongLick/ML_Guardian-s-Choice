using UnityEngine;

public class PathDraw : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] PathFinding pathFinding;
	[SerializeField] LineRenderer lineRenderer;

	public void Drawing()
	{
		lineRenderer.positionCount = pathFinding.FinalNodeList.Count;

		for (int i = 0; i < pathFinding.FinalNodeList.Count; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(pathFinding.FinalNodeList[i].X, pathFinding.FinalNodeList[i].Y, 0));
		}
	}
}
