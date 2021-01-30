using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NarrarationObject : MonoBehaviour
{
    public void Narrarate(string narraration)
    {
        Narrarator.instance.Action(narraration);        
    }
}
