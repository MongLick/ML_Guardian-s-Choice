using UnityEngine;

[System.Serializable]
public class Node
{
	[Header("Components")]
	[SerializeField] Node parentNode;
	public Node ParentNode { get { return parentNode; } set { parentNode = value; } }

	[Header("Specs")]
	[SerializeField] int x;
	public int X { get { return x; } }
	[SerializeField] int y;
	public int Y { get { return y; } }
	[SerializeField] int g;
	public int G { get { return g; } set { g = value; } }
	[SerializeField] int h;
	public int H { get { return h; } set { h = value; } }
	[SerializeField] int f;
	public int F { get { return g + h; } }
	[SerializeField] bool isWall;
	public bool IsWall { get { return isWall; } }

	public Node(bool _isWall, int _x, int _y)
	{
		isWall = _isWall;
		x = _x;
		y = _y;
	}
}