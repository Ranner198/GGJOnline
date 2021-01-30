using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public string Room;
    public static GameStateManager instance;
    [SerializeField]
    public List<string> StateMap = new List<string>();

    private void Awake()
    {
        instance = this;
    }

    public static bool Is(string Keyword)
    {
        if (instance.StateMap.Contains(Keyword))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Set(string Keyword)
    {
        if (Keyword != "")
        {
            instance.StateMap.Add(Keyword);
        }
    }

    public static void Clear (string Keyword)
    {
        if (Keyword != "")
        {
            instance.StateMap.Remove(Keyword);
        }
    }
}
