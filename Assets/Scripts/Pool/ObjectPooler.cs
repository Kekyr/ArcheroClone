using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private List<PoolSO> pools = new List<PoolSO>();

    public Dictionary<string, Queue<Projectile>> poolDictionary = new Dictionary<string, Queue<Projectile>>();

    public void AddPool(PoolSO pool)
    {
        Queue<Projectile> objectPool;
        GameObject poolParent;

        if (!poolDictionary.ContainsKey(pool.Tag))
        {
            poolParent = new GameObject(pool.Tag);
            poolParent.transform.parent = transform;

            objectPool = new Queue<Projectile>();
            poolDictionary.Add(pool.Tag, objectPool);
        }
        else
        {
            objectPool = poolDictionary[pool.Tag];
            poolParent = objectPool.Peek().gameObject.transform.parent.gameObject;
        }

        CreateInstances(pool, objectPool, poolParent);
    }

    private void CreateInstances(PoolSO pool, Queue<Projectile> objectPool, GameObject poolParent)
    {
        for (int i = 0; i < pool.NumberPerInstance; i++)
        {
            Projectile projectile = Instantiate(pool.Prefab, poolParent.transform);
            projectile.gameObject.SetActive(false);
            objectPool.Enqueue(projectile);
        }
    }

    public Projectile SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        Projectile projectileToSpawn = poolDictionary[tag].Dequeue();

        projectileToSpawn.gameObject.SetActive(true);
        projectileToSpawn.transform.position = position;
        projectileToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = projectileToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(projectileToSpawn);

        return projectileToSpawn;
    }
}