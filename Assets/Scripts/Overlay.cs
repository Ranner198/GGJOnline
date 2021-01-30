using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public static Overlay instance;
    public string CurrentPopup;

    [SerializeField]
    private PopupImage[] PopupImages;

    // Start is called before the first frame update
    void Awake()
    {
        PopupImages = GetComponentsInChildren<PopupImage>(true);
        instance = this;
    }

    // Image to show or "None" for none of them
    public void ShowOnlyPopupImage(string ID)
    {
        foreach (PopupImage Popup in PopupImages)
        {
            Popup.gameObject.SetActive(Popup.ID == ID);
        }
        CurrentPopup = ID;
    }
}
