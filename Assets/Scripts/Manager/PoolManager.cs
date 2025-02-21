using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
	private Dictionary<int, ObjectPool> poolDic = new Dictionary<int, ObjectPool>();
	private List<GameObject> destroyObject = new List<GameObject>();

	public bool IsPoolCreated(PooledObject prefab)
	{
		return poolDic.ContainsKey(prefab.GetInstanceID());
	}

	public void CreatePool(PooledObject prefab, int size, int capacity)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = $"Pool_{prefab.name}";

		ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.CreatePool(prefab, size, capacity);

		poolDic.Add(prefab.GetInstanceID(), objectPool);

		destroyObject.Add(gameObject);
	}

	public void DestroyPool()
	{
		foreach (GameObject poolObject in destroyObject)
		{
			Destroy(poolObject);
		}
	}

	public void DestroyPool(PooledObject prefab)
	{
		ObjectPool objectPool = poolDic[prefab.GetInstanceID()];
		Destroy(objectPool.gameObject);

		poolDic.Remove(prefab.GetInstanceID());
	}

	public void ClearPool()
	{
		foreach (ObjectPool objectPool in poolDic.Values)
		{
			Destroy(objectPool.gameObject);
		}

		poolDic.Clear();
	}

	public PooledObject GetPool(PooledObject prefab, Vector3 position, Quaternion rotation)
	{
		return poolDic[prefab.GetInstanceID()].GetPool(position, rotation);
	}
}
