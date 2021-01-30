using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public string Room;
    public GameStateManager instance;

    private void Awake()
    {
        instance = this;
    }
}
