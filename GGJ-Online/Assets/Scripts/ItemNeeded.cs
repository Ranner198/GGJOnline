using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemNeeded : MonoBehaviour
{
    public GameObject go;
    public UnityEvent Completed;

    public bool Use(GameObject go) 
    {
        if (this.go == go)
        {
            Completed.Invoke();
            return true;
        }
        else
            return false;
    }
}
