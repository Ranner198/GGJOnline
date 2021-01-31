using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldog : MonoBehaviour
{
    public Transform RopeStart;
    public Transform RopeEnd;
    public GameObject Rope;
    public Transform NeckStart;
    public Transform PoleEnd;

    // Notes
    public float length;
    public float calcLen;
    public float scaleX;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 start = RopeStart.position;
        Vector3 end = RopeEnd.position;

        length = (end - start).magnitude; // Corresponds to 1.0f in scale
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = NeckStart.position;
        Vector3 end = PoleEnd.position;
        Rope.transform.position = start;
        Rope.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2((end-start).y, (end-start).x)));
        calcLen = (end - start).magnitude;
        scaleX = calcLen / length;
        Rope.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
    }
}
