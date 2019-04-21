using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3RandomItemFactory : Match3ItemFactory
{
    [SerializeField] private RuntimePool[] pools;

    public override GameObject GetItem()
    {
        return pools[Random.Range(0,pools.Length-1)].GetItem();
    }

    public override void DestroyItem(GameObject item)
    {
        RuntimePoolObject poolObject = item.GetComponent<RuntimePoolObject>();
        
        if (poolObject == null)
        {
            Destroy(item);
            return;
        }

        poolObject.Recycle();
    }
}
