using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldog : MonoBehaviour
{
    public bool IsBlocking = true;

    public Transform RopeStart;
    public Transform RopeEnd;
    public GameObject Rope;
    public Transform NeckStart;
    public Transform PoleEnd;

    // Notes
    private float length;
    private float calcLen;
    private float scaleX;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 start = RopeStart.position;
        Vector3 end = RopeEnd.position;

        length = (end - start).magnitude; // Corresponds to 1.0f in scale
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBlocking)
        {
            transform.position = new Vector3(Movement.instance.gameObject.transform.position.x, startPosition.y + Movement.instance.gameObject.transform.position.x/40.0f, startPosition.z);
        }

        Vector3 start = NeckStart.position;
        Vector3 end = PoleEnd.position;
        Rope.transform.position = start;
        Rope.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2((end-start).y, (end-start).x)));
        calcLen = (end - start).magnitude;
        scaleX = calcLen / length;
        Rope.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);

    }
}
