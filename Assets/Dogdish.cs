using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogdish : MonoBehaviour
{
    public GameObject Steak;

    public void SteakInBowl()
    {
        Steak.SetActive(true);
    }
}
