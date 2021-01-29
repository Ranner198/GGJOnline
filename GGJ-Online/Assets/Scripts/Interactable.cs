using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onClickEvent;
    public bool addToInventory, deactivateOnClick, deactiveColliderOnClick;

    public string Name;
    public int Quanity;
    public Sprite sprite;

    void OnMouseDown()
    {
        print("clicked on " + this.gameObject);
        if (addToInventory)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().AddItem(Name, Quanity, sprite, this.gameObject);
        if (deactiveColliderOnClick)
            GetComponent<BoxCollider2D>().enabled = false;
        if (deactivateOnClick)
            gameObject.SetActive(false);        
        onClickEvent.Invoke();
    }
}
