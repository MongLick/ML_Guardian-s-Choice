using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
	private Dictionary<int, ObjectPool> poolDic = new Dictionary<int, ObjectPool>();

	public bool IsPoolCreated(PooledObject prefab)
	{
		return poolDic.ContainsKey(prefab.GetInstanceID());
	}

	public void CardCreatePool(PooledObject prefab, int size, int capacity)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = $"Pool_{prefab.name}";

		RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(1, 1);
		rectTransform.anchoredPosition = Vector2.zero;

		ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.CreatePool(prefab, size, capacity);

		poolDic.Add(prefab.GetInstanceID(), objectPool);
		objectPool.transform.SetParent(Manager.Card.CardParent, false);
	}

	public void CreatePool(PooledObject prefab, int size, int capacity)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = $"Pool_{prefab.name}";

		ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.CreatePool(prefab, size, capacity);

		poolDic.Add(prefab.GetInstanceID(), objectPool);
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
