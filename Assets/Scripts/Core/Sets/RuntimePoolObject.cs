using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimePoolObject : MonoBehaviour
{
    RuntimePool pool;

    public void SetPool(RuntimePool pool)
    {
        this.pool = pool;
    }

    public void Recycle()
    {
        pool.Recycle(this);
    }
}
