using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : BaseBehaviour where T : PoolObj
{
    [Header("Spawner")]
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected PoolHolder poolHolder;
    [SerializeField] protected Transform prefab;
    [SerializeField] protected List<T> poolObjsList = new();
    [SerializeField] protected List<T> prefabs = new();

    // Fast O(1) lookup dictionaries
    protected readonly Dictionary<string, Queue<T>> poolDict = new();
    protected readonly Dictionary<string, T> prefabDict = new();

    protected override void Awake()
    {
        base.Awake();
        this.InitializePool();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPoolHolder();
        this.LoadPrefabs();
    }

    protected void InitializePool()
    {
        this.prefabDict.Clear();
        foreach (T child in this.prefabs)
        {
            if (child == null) continue;
            string key = child.GetName();
            if (!this.prefabDict.ContainsKey(key))
            {
                this.prefabDict[key] = child;
            }
            if (!this.poolDict.ContainsKey(key))
            {
                this.poolDict[key] = new Queue<T>();
            }
        }
    }

    protected void LoadPoolHolder()
    {
        if (this.poolHolder != null) return;
        this.poolHolder = GetComponentInChildren<PoolHolder>();
    }

    protected void LoadPrefab()
    {
        if (this.prefab != null) return;
        this.prefab = transform.Find("Prefab");
    }

    protected void LoadPrefabs()
    {
        this.LoadPrefab();
        if (this.prefabs.Count > 0 || this.prefab == null) return;

        foreach (Transform child in this.prefab)
        {
            T prefabT = child.GetComponent<T>();
            if (prefabT != null)
            {
                this.prefabs.Add(prefabT);
                prefabT.gameObject.SetActive(false);
            }
        }
        this.InitializePool();
    }

    public virtual T Spawn(string prefabName, Vector2 pos, CharacterCtrl owner = null)
    {
        T targetPrefab = GetPrefabByName(prefabName);
        if (targetPrefab == null) return null;
        return Spawn(targetPrefab, pos, owner);
    }

    public virtual T Spawn(T targetPrefab, Vector2 pos, CharacterCtrl owner = null)
    {
        T newObj = Spawn(targetPrefab);
        newObj.transform.position = pos;
        if (this.poolHolder != null)
        {
            newObj.transform.SetParent(this.poolHolder.transform, false);
            newObj.transform.position = pos;
        }
        newObj.gameObject.SetActive(true);
        if (owner != null) newObj.Init(owner);
        return newObj;
    }

    public virtual T Spawn(T targetPrefab)
    {
        T newObj = this.GetObjFromPool(targetPrefab);
        if (newObj == null)
        {
            this.spawnCount++;
            newObj = Instantiate(targetPrefab);
            this.UpdateName(targetPrefab.transform, newObj.transform);
        }
        return newObj;
    }

    protected virtual T GetPrefabByName(string prefabName)
    {
        if (this.prefabDict.TryGetValue(prefabName, out T foundPrefab))
            return foundPrefab;

        foreach (T child in this.prefabs)
        {
            if (child != null && child.GetName() == prefabName)
            {
                this.prefabDict[prefabName] = child;
                return child;
            }
        }
        return null;
    }

    public void Despawn(T obj)
    {
        if (obj == null) return;
        obj.gameObject.SetActive(false);
        this.AddObjIntoPool(obj);
    }

    protected void AddObjIntoPool(T obj)
    {
        string key = obj.GetName();
        if (!this.poolDict.TryGetValue(key, out Queue<T> queue))
        {
            queue = new Queue<T>();
            this.poolDict[key] = queue;
        }
        queue.Enqueue(obj);

        if (!this.poolObjsList.Contains(obj))
        {
            this.poolObjsList.Add(obj);
        }
    }

    protected virtual void UpdateName(Transform targetPrefab, Transform newObject)
    {
        newObject.name = targetPrefab.name + "_" + this.spawnCount;
    }

    protected T GetObjFromPool(T targetPrefab)
    {
        string key = targetPrefab.GetName();
        if (this.poolDict.TryGetValue(key, out Queue<T> queue) && queue.Count > 0)
        {
            T poolObj = queue.Dequeue();
            this.poolObjsList.Remove(poolObj);
            return poolObj;
        }
        return null;
    }
}
