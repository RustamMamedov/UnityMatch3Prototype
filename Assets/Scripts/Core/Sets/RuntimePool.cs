using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="RuntimePool")]
public class RuntimePool : ScriptableObject
{
    [SerializeField] private RuntimePoolObject prefab;
    [SerializeField, Range(2, 50)] private int maxPoolObjects = 10;
    private Stack<RuntimePoolObject> stack = new Stack<RuntimePoolObject>();


    public GameObject GetItem()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Please set up pool prefab");
            return null;
        }

        if (stack.Count == 0)
            return CreatePoolObject();

        GameObject item = stack.Pop().gameObject;
        item.SetActive(true);
        return item;
    }

    private GameObject CreatePoolObject()
    {
        RuntimePoolObject newItem = GameObject.Instantiate(prefab);
        newItem.SetPool(this);
        return newItem.gameObject;
    }

    public void Recycle(RuntimePoolObject item)
    {
        if (stack.Count > maxPoolObjects)
        {
            GameObject.Destroy(item.gameObject);
            return;
        }

        item.gameObject.SetActive(false);
        stack.Push(item);
    }

    public void Clear()
    {
        stack.Clear();
    }
}
