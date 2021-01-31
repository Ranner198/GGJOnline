using System;
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
    public bool IsFed = false;
    private Vector3 MeatPosition;
    public float Speed = 50.0f;
    private SpriteRenderer Renderer;
    private Vector3 LastPositionFaced;
    public Transform BowlLocation;
    public GameObject Barrier1;

    public void EventFed(Vector3 MeatLocation)
    {
        IsBlocking = false;
        MeatPosition = MeatLocation;
    }

    public Transform PoleEnd;

    // Notes
    private float length;
    private float calcLen;
    private float scaleX;
    private Vector3 startPosition;


    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 start = RopeStart.position;
        Vector3 end = RopeEnd.position;

        length = (end - start).magnitude; // Corresponds to 1.0f in scale
        startPosition = transform.position;
        LastPositionFaced = transform.position;
        if (GameStateManager.Is("DogFed"))
        {
            IsBlocking = false;
            MeatPosition = BowlLocation.position;
            transform.position = new Vector3(MeatPosition.x, startPosition.y + MeatPosition.x / 40.0f, startPosition.z);
            LowerBarriers();
        }
    }

    public void LowerBarriers()
    {
        Barrier1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetLocation;

        // Determine where to move the dog to
        if (IsBlocking)
        {
            TargetLocation = /*transform.position = */new Vector3(Movement.instance.gameObject.transform.position.x, startPosition.y + Movement.instance.gameObject.transform.position.x/40.0f, startPosition.z);
        }
        else
        {
            MeatPosition = BowlLocation.position;
            // Move toward the steak horizontally and stop
            TargetLocation = new Vector3(MeatPosition.x, startPosition.y + MeatPosition.x / 40.0f, startPosition.z);
        }

        {
            float distance = Vector2.Distance(transform.position, TargetLocation);
            if (distance > 0.1f)
            {
                transform.Translate((TargetLocation - transform.position).normalized * Time.deltaTime * Speed);
                // if (anim != null) anim.SetFloat("Vel", distance);
            }
            else
            {
                // if (anim != null) anim.SetFloat("Vel", 0.0f);
            }
            Vector3 direction = TargetLocation - transform.position;
            if (direction.x >= 0.2f) // Dog Face Right
            {
                Renderer.flipX = false;
                LastPositionFaced = transform.position;
            }
            else if (direction.x <= -0.2f) // Dog Face Left
            {
                Renderer.flipX = true;
                LastPositionFaced = transform.position;
            }
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
