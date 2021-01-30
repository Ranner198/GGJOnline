using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPopup : MonoBehaviour
{
    public string popupID;
    public bool haltMovement = true;
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
        print("TouchPopup: Triggering with " + coll.gameObject.tag);
        if (ReadyForCollisions)
        {
            if (coll.transform.tag == "Player")
            {
                print("Showing popup " + popupID);
                Overlay.instance.ShowOnlyPopupImage(popupID);
                if (haltMovement)
                {
                    coll.GetComponent<Movement>().HaltMovement();
                }
            }
        }
    }
}
