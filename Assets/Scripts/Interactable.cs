using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onClickEvent;
    public bool addToInventory, deactivateOnClick, deactiveColliderOnClick;

    public string Name;
    public int Quanity;
    public Sprite sprite;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            if (this.gameObject == go)
                Destroy(this.gameObject);
        }
    }

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
