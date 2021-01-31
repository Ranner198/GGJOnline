using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemNeeded : MonoBehaviour
{
    public string go;
    public UnityEvent Completed;
    public UnityEvent UnCompleted;

    public bool Use(string go) 
    {
        if (this.go == go)
        {
            Completed.Invoke();
            return true;
        }
        else
        {
            UnCompleted.Invoke();
            return false;
        }
    }

    public bool CanUse(string go)
    {
        if (this.go == go)
        {
            return true;
        }
        else
            return false;
    }
}
