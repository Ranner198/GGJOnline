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

    public int numberOfInventorySlots = 10;

    [SerializeField]
    private GameObject inHand;
    private int Index;

    public void Start()
    {
        camera = Camera.main;
        foreach (InventoryItem item in inventoryItems)
        {
            item.inventoryRef = this;
        }
    }
    public void AddItem(string Name, int quantity, Sprite sprite, GameObject go) 
    {
        bool found = false;
        print("hello>");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Name == "")
            {
                inventoryItems[i].SetProps(i, Name, quantity, sprite, go);
                found = true;
                break;
            }
        }
        if (!found) { print("ERROR no room in inventory..."); }
    }

    public void UseItem(int index)
    {
        if (inHand == null) 
        {
            inHand = Instantiate(inventoryItems[index].SpawnInteractable(), transform.position, Quaternion.identity);
            inHand.GetComponent<Collider2D>().enabled = false;
            Index = index;
        }
    }

    public void Update()
    {
        if (inHand != null)
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition); ;
            mousePos.z = 0;
            inHand.transform.position = mousePos;

            if (Input.GetMouseButton(0))
            {
                Use(mousePos);
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
            print(hit.transform.tag);
            var itmNeeded = hit.transform.GetComponent<ItemNeeded>();            
            if (itmNeeded != null)
            {
                if (itmNeeded.Use(inHand.GetComponent<Interactable>().Name))
                {                    
                    inventoryItems[Index].UseItem();
                    Destroy(inHand);
                }
            }
            i++;
                Destroy(inHand);
        }        
    }
}
