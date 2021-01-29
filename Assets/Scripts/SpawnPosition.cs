using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public static SpawnPosition instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;        
    }
    public void Spawned(Transform playersTransform, string lastSceneName) 
    {
        foreach (var spawn in spawnPoints)
        {
            if (lastSceneName == spawn.sceneName)
                playersTransform.position = spawn.spawnPosition.position;
        }
    }
}

[System.Serializable]
public class SpawnPoint {
    public string sceneName;
    public Transform spawnPosition;
}
