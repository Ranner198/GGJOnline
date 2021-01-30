using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntry : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
