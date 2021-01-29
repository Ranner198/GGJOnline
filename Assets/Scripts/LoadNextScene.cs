using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string nextScene;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        print(coll.gameObject.tag);
        if (coll.transform.tag == "Player")
        {
            PlayerRef.instance.lastSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(nextScene);
        }
    }
}
