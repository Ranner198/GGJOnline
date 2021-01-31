using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMethods : MonoBehaviour
{
    public GameObject child;
    public bool isUp = false;
    public Movement movement;
    public void Update()
    {
        isUp = child.activeSelf;

        if (isUp)
            movement.HaltMovement();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            child.SetActive(!child.activeSelf);
        }
    }
    public void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Exit() 
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
