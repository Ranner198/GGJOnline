using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField]
    private List<SpawnEntry> spawnPoints = new List<SpawnEntry>();

    public void Awake()
    {
        SpawnEntry[] Entries = FindObjectsOfType<SpawnEntry>();
        foreach (SpawnEntry Entry in Entries)
        {
            spawnPoints.Add(Entry);
        }
    }

    public Vector3 Spawned(Transform playersTransform, string lastSceneName) 
    {
        foreach (var spawn in spawnPoints)
        {
            //print("Looking for spawn for: " + spawn.sceneName + "/" + lastSceneName);
            if (lastSceneName == spawn.sceneName)
            {
                playersTransform.position = spawn.transform.position;
                return spawn.transform.position;
            }
        }

        // Default to middle if not found
        Debug.LogWarning("No start position found! Going to start");
        return Vector3.zero;
    }
}
