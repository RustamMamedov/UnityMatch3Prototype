using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Match3ItemFactory : MonoBehaviour
{
    public abstract GameObject GetItem();
    
    public virtual void DestroyItem(GameObject item)
    {
        Destroy(item);
    }
}
