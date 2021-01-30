using UnityEngine;
using System.Collections;

public class PersistManager : MonoBehaviour {

	public GameObject persistPrefab;    
	public GameObject persistObject;

    void Awake () 
	{
		GameObject persist = GameObject.Find ("Persistent");
		if (persist == null) 
		{
            if (persistPrefab) persistObject = (GameObject)Instantiate(persistPrefab);
			persistObject.name="Persistent";
        }
	}
}
