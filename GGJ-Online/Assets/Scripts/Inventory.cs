using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public new Camera camera;
    public EventSystem eventSystem;
    public List<InventoryItem> inventoryItems;    

    public int numberOfInventorySlots = 10;

    [SerializeField]
    private GameObject inHand;
    private int Index;

    public void AddItem(string Name, int quantity, Sprite sprite, GameObject go) 
    {
        bool found = false;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Name == "")
            {
                print(i);
                inventoryItems[i].SetProps(i, Name, quantity, sprite, go);
                found = true;
                break;
            }
        }
        if (!found) { print("ERROR no room in inventory..."); }
    }

    public void UseItem(int index)
    {
        print("CLicked");
        inHand = inventoryItems[index].SpawnInteractable();
        Index = index;
        inHand.SetActive(true);
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
        RaycastHit[] hits;
        hits = Physics.RaycastAll(mousePos, Vector3.forward, 100.0F);
        Debug.DrawRay(mousePos, Vector3.forward * 100);
        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit hit = hits[i];
            var itmNeeded = hit.transform.GetComponent<ItemNeeded>();            
            if (itmNeeded != null)
            {
                if (itmNeeded.Use(inHand))
                {
                    Destroy(inHand);
                    inventoryItems[Index].UseItem();
                }
            }
            i++;
        }
    }
}
