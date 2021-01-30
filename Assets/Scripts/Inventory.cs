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

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

    [SerializeField]
    private GameObject inHand;
    [SerializeField]
    private int Index;
    private bool usingItem = false;

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
        Movement.instance.HaltMovement();
    }

    public void UseItem(int index)
    {
        if (inHand == null) 
        {
            inHand = Instantiate(inventoryItems[index].SpawnInteractable(), transform.position, Quaternion.identity);
            inHand.GetComponent<Collider2D>().enabled = false;
            Index = index;
            Movement.instance.HaltMovement();
            usingItem = false;
        }
    }

    public void Update()
    {
        if (inHand != null)
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition); ;
            mousePos.z = 0;
            inHand.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0) && usingItem)
            {
                Use(mousePos);
                usingItem = false;
            }
            usingItem = true;
        }
    }

    public void Use(Vector3 mousePos) 
    {
        mousePos.z = -10;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(mousePos, Vector3.forward, 100.0F);
        Debug.DrawRay(mousePos, Vector3.forward * 100);
        int i = 0;


        // Check if hitting UI
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = this.transform.localPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        if (results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);


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
                }
            }
            i++;
        }
        Destroy(inHand);
        Movement.instance.HaltMovement();
    }
}
