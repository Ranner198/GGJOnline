using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPhone : MonoBehaviour
{
    public GameObject LivePhone;

    // Start is called before the first frame update
    public void PhoneHasBatteryNow()
    {
        //GetComponent<TouchPopup>().DisablePopup = true;
        Overlay.instance.ShowOnlyPopupImage("");
        LivePhone.SetActive(true);
        Destroy(gameObject);
        GameStateManager.Set("DeadPhoneIsNowLive");
    }

    private void Start()
    {
        if (GameStateManager.Is("DeadPhoneIsNowLive"))
        {
            PhoneHasBatteryNow();
        }
    }
}
