using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private GameEvent OnUpdate;
    [SerializeField] private GameEvent OnFixedUpdate;
    [SerializeField] private GameEvent OnLateUpdate;


    private void Update()
    {
        OnUpdate?.InvokeEvent();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.InvokeEvent();
    }

    private void LateUpdate()
    {
        OnLateUpdate?.InvokeEvent();
    }
}
