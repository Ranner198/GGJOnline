using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public int Index;
    public string Name;
    public int? Quantity;
    public Sprite Sprite;
    public GameObject Interactable;

    public Inventory inventoryRef;

    public TextMeshProUGUI nameGUI, quantityGUI;
    public Image image;

    public void UseItem()
    {        
        this.Name = "";
        this.Quantity = null;
        this.Sprite = null;
        this.Interactable = null;
        UpdateGUI();
    }
    public void SetProps(int Index, string Name, int Quantity, Sprite Sprite, GameObject go)
    {
        this.Index = Index;
        this.Name = Name;
        this.Quantity = Quantity;
        this.Sprite = Sprite;
        this.Interactable = go;
        UpdateGUI();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable != null)
        {
            inventoryRef.UseItem(this.Index);
        }
    }
    public void UpdateGUI()
    {
        this.nameGUI.text = this.Name;
        this.quantityGUI.text = this.Quantity.ToString();
        this.image.sprite = this.Sprite;
    }
    public GameObject SpawnInteractable() 
    {
        //print(Interactable.name);
        return Interactable;
    }
}
