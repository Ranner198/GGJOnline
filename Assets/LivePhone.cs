using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePhone : MonoBehaviour
{
    public void PhonedHome()
    {
        GameStateManager.Set("PhonedHome");
    }
}
