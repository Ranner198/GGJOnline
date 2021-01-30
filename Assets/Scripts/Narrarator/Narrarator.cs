using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Narrarator : MonoBehaviour
{
    public static Narrarator instance;
    public TextMeshProUGUI narrarationText;
    public Image background;
    public float showTime;

    public void Awake()
    {
        instance = this;
        background.CrossFadeAlpha(0, Time.deltaTime, true);
    }

    public void Action(string output) 
    {
        StopAllCoroutines();
        StartCoroutine(WaitForSeconds(output));
    }

    IEnumerator WaitForSeconds(string output)
    {
        narrarationText.text = output;
        background.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSecondsRealtime(showTime);
        background.CrossFadeAlpha(0, 1, true);
    }
}
