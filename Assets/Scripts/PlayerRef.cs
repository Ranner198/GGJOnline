using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRef : MonoBehaviour
{
    public static PlayerRef instance;
    public string lastSceneName;
    public new Camera camera;
    public Movement movement;
    void Start()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(camera);
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        movement.newPosition = SpawnPosition.instance.Spawned(this.transform, lastSceneName);      
    }
}
