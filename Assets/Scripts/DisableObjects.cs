using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjects : MonoBehaviour
{
    public List<GameObject> turnOff = new List<GameObject>();
    public List<GameObject> turnOn = new List<GameObject>();

    void Start()
    {
        foreach (GameObject off in turnOff)
            off.SetActive(false);
        foreach (GameObject on in turnOn)
            on.SetActive(true);
    }
}
