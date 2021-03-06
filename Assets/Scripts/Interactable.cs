﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onClickEvent;
    public bool addToInventory, DestoryOnClick, deactiveColliderOnClick;
    public string TakenState;

    public string Name;
    public int Quanity;
    public Sprite sprite;
    public GameObject ItemSpawn;

    public void Start()
    {
        if (GameStateManager.Is(TakenState))
        {
            // This item is disabled, just Destroy and stop here
            Destroy(gameObject);
        }
        else
        {
            //DontDestroyOnLoad(this.gameObject);
//            SceneManager.sceneLoaded += OnSceneLoaded;
        }
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
        if (addToInventory)
        {
            if (String.IsNullOrEmpty(Inventory.instance.inventoryItems[0].Name))
            {
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().AddItem(Name, Quanity, sprite, ItemSpawn);
                GameStateManager.Set(TakenState);
            }
            else
            {
                Narrarator.instance.Action("Inventory Full");
                Movement.instance.HaltMovement();
                return;
            }
        }
        else if (GetComponent<TouchPopup>())
        {
            GetComponent<TouchPopup>().Show();
        }
        if (deactiveColliderOnClick)
            GetComponent<BoxCollider2D>().enabled = false;
        if (DestoryOnClick)
            Destroy(gameObject, Time.deltaTime);
        onClickEvent.Invoke();
        Movement.instance.HaltMovement();
    }
}
