using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogdish : MonoBehaviour
{
    public GameObject Steak;

    public void SteakInBowl()
    {
        Steak.SetActive(true);
        Bulldog Dog = FindObjectOfType<Bulldog>();
        Dog.EventFed(Steak.transform.position);
        GameStateManager.Set("DogFed");
        Dog.LowerBarriers();
    }

    private void Start()
    {
        if (GameStateManager.Is("DogFed"))
        {
            Steak.SetActive(true);
        }
    }
}
