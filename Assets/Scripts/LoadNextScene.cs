using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string nextScene;
    public bool ReadyForCollisions = false;
    public bool Hide = true;

    public void Start()
    {
        StartCoroutine(ReadyNow());
        if (Hide)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    IEnumerator ReadyNow()
    {
        yield return new WaitForSeconds(0.5f);
        ReadyForCollisions = true;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        print(coll.gameObject.tag);
        if (ReadyForCollisions)
        {
            if (coll.transform.tag == "Player")
            {
                GameStateManager.instance.lastSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
