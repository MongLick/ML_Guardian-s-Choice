using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] PooledObject prefab;

	[Header("Specs")]
	[SerializeField] int size;
	[SerializeField] int capacity;

	private Stack<PooledObject> objectPool;

	public void CreatePool(PooledObject prefab, int size, int capacity)
	{
		this.prefab = prefab;
		this.size = size;
		this.capacity = capacity;

		objectPool = new Stack<PooledObject>(capacity);
		for (int i = 0; i < size; i++)
		{
			PooledObject instance = Instantiate(prefab);
			instance.gameObject.SetActive(false);
			instance.Pool = this;
			instance.transform.SetParent(transform, false);
			objectPool.Push(instance);
		}
	}

	public PooledObject GetPool(Vector3 position, Quaternion rotation)
	{
		PooledObject instance;

		if (objectPool.Count > 0)
		{
			instance = objectPool.Pop();
			instance.transform.position = position;
			instance.transform.rotation = rotation;
			instance.gameObject.SetActive(true);
		}
		else
		{
			instance = Instantiate(prefab);
			instance.Pool = this;
			instance.transform.position = position;
			instance.transform.rotation = rotation;
			instance.transform.SetParent(transform, false);
			objectPool.Push(instance);
		}

		return instance;
	}

	public void ReturnPool(PooledObject instance)
	{
		if (objectPool.Count < capacity)
		{
			instance.gameObject.SetActive(false);
			objectPool.Push(instance);
		}
		else
		{
			Destroy(instance.gameObject);
		}
	}
}
