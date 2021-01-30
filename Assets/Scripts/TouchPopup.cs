using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPopup : MonoBehaviour
{
    public string popupID;
    public bool haltMovement = true;
    public bool ignoreWalkOver = false;
    public bool ReadyForCollisions = false;

    public void Start()
    {
        StartCoroutine(ReadyNow());
    }
    IEnumerator ReadyNow()
    {
        yield return new WaitForSeconds(0.5f);
        ReadyForCollisions = true;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (!ignoreWalkOver)
        {
            print("TouchPopup: Triggering with " + coll.gameObject.tag);
            if (ReadyForCollisions)
            {
                if (coll.transform.tag == "Player")
                {
                    Show();
                }
            }
        }
    }

    public void Show()
    {
        // Only show a pop if none are showing
        if (Overlay.instance.CurrentPopup == "")
        {
            print("Showing popup " + popupID);
            Overlay.instance.ShowOnlyPopupImage(popupID);
            if (haltMovement)
            {
                Movement.instance.HaltMovement();
            }
        }
    }
}
