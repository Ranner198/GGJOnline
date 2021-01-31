using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvokeAfterSeconds : MonoBehaviour
{
    public UnityEvent onAfterSeconds;
    public float seconds;
    void Start()
    {
        StartCoroutine(waitForSeconds());
    }

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(seconds);

        onAfterSeconds.Invoke();
    }
}
