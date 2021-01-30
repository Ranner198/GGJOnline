using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupImage : MonoBehaviour, IPointerDownHandler
{
    public string ID;
    public string NextID;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);

        // Close image
        Overlay.instance.ShowOnlyPopupImage(NextID);
    }
}
