using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spawner<T> : DinoBehaviour where T : PoolObj
{
    [Header("Spawner")]
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected PoolHolder poolHolder;
    [SerializeField] protected List<T> poolObjsList = new();
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPoolHolder();
    }

    protected void LoadPoolHolder()
    {
        if (this.poolHolder != null) return;
        this.poolHolder = GetComponentInChildren<PoolHolder>();
        Debug.Log(transform.name + ": LoadPoolHolder", gameObject);
    }
    public virtual T Spawn(T prefab, Vector2 pos)
    {
        T new_Obj = Spawn(prefab);
        new_Obj.transform.position = pos;
        new_Obj.gameObject.SetActive(true);
        new_Obj.transform.parent = this.poolHolder.transform;
        return new_Obj;
    }
    public virtual T Spawn(T prefab)
    {
        T new_Obj = this.GetObjFromPool(prefab);
        if (new_Obj == null)
        {
            this.spawnCount++;
            new_Obj = Instantiate(prefab);
            this.UpdateName(prefab.transform, new_Obj.transform);
        }
        return new_Obj;
    }
    public void Despawn(T obj)
    {
        if (obj is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            this.AddObjIntoPool(obj);
        }
    }

    protected void AddObjIntoPool(T obj)
    {
        this.poolObjsList.Add(obj);
    }

    protected void RemoveObjIntoPool(T obj)
    {
        this.poolObjsList.Remove(obj);
    }
    protected virtual void UpdateName(Transform prefab, Transform newObject)
    {
        newObject.name = prefab.name + "_" + this.spawnCount;
    }
    protected T GetObjFromPool(T prefab)
    {
        foreach (T poolObj in this.poolObjsList)
        {
            if (poolObj.GetName() == prefab.GetName())
            {
                this.RemoveObjIntoPool(poolObj);
                return poolObj;
            }
        }
        return null;
    }
}
