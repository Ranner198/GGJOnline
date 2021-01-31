using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfState : MonoBehaviour
{
    public string key;

    public void Awake()
    {
        if (GameStateManager.Is(key))
        {
            Destroy(gameObject);
        }
    }

    public void SetState()
    {
        GameStateManager.Set(key);
    }
}
