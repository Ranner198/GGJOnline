using UnityEngine;
using System.Collections;

public class PersistManager : MonoBehaviour {

	public GameObject persistPrefab;    
	public GameObject persistObject;
	public bool DestroyPersist = false;

    void Awake () 
	{
		GameObject persist = GameObject.Find ("Persistent");
		if (DestroyPersist)
		{
			if (persist)
				Destroy(persist);
		}
		else
		{
			if (persist == null)
			{
				if (persistPrefab) persistObject = (GameObject)Instantiate(persistPrefab);
				persistObject.name = "Persistent";
			}
		}
	}
}
