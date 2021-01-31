using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    private new Camera camera;
    public EventSystem eventSystem;
    public List<InventoryItem> inventoryItems;

    public static Inventory instance;

    public int numberOfInventorySlots = 10;

    [SerializeField]
    private GameObject inHand;
    private int Index;
    [SerializeField]
    private bool usingItem = false;

    public void Start()
    {
        instance = this;
        camera = Camera.main;
        foreach (InventoryItem item in inventoryItems)
        {
            item.inventoryRef = this;
        }
    }
    public void AddItem(string Name, int quantity, Sprite sprite, GameObject go) 
    {
        bool found = false;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Name == "")
            {
                print("Setting prop " + i);
                inventoryItems[i].SetProps(i, Name, quantity, sprite, go);
                found = true;
                break;
            }
        }
        if (!found) { print("ERROR no room in inventory..."); }
        Movement.instance.HaltMovement();
    }

    public void UseItem(int index)
    {
        if (inHand == null) 
        {
            print("Grabbing Inventory");
            inHand = Instantiate(inventoryItems[index].SpawnInteractable(), transform.position, Quaternion.identity);
            inHand.GetComponent<Collider2D>().enabled = false;
            Index = index;
            Movement.instance.HaltMovement();
            usingItem = false;
        }
    }

    public void Update()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (inHand != null)
        {                        
            inHand.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0) && usingItem)
            {
                Use(mousePos);
                usingItem = false;
            }
            usingItem = true;
        }
        else
        {
            // Allow user to click on objects without things in their hands
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D[] hits;
                hits = Physics2D.RaycastAll(mousePos, Vector3.forward, 100.0F);
                Debug.DrawRay(mousePos, Vector3.forward * 100);
                int i = 0;

                while (i < hits.Length)
                {
                    RaycastHit2D hit = hits[i];
                    print("Use " + Index + ": " + hit.transform.name + " tag=" + hit.transform.tag);
                    var itmNeeded = hit.transform.GetComponent<ItemNeeded>();
                    if (itmNeeded != null)
                    {
                        itmNeeded.EmptyClicked();
                    }
                    i++;
                }
            }
        }
    }

    public void Use(Vector3 mousePos) 
    {
        mousePos.z = -10;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(mousePos, Vector3.forward, 100.0F);
        Debug.DrawRay(mousePos, Vector3.forward * 100);
        int i = 0;

        while (i < hits.Length)
        {            
            RaycastHit2D hit = hits[i];
            print("Use " + Index + ": " + hit.transform.name + " tag=" + hit.transform.tag);
            var itmNeeded = hit.transform.GetComponent<ItemNeeded>();            
            if (itmNeeded != null)
            {
                if (itmNeeded.CanUse(inHand.GetComponent<Interactable>().Name))
                {
                    inventoryItems[Index].UseItem();
                    if (itmNeeded.Use(inHand.GetComponent<Interactable>().Name))
                    {
                        Destroy(inHand);                        
                    }
                }
            }
            i++;
        }
        Destroy(inHand);
        Movement.instance.HaltMovement();
    }
}
