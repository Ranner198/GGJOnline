using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyWalking : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public Transform LeavePoint;
    public bool IsGoingRight;
    public float Speed = 5f;
    public float Percent = 0.0f;
    public bool DidDropPhone = false;
    public bool IsPausing = false;
    public float TimeTilPause = 3.0f;
    public float PauseLength = 1.0f;
    private float PauseTimeRemaining;
    public bool IsLeaving;
    private SpriteRenderer Renderer;
    public GameObject Phone;
    public bool IsDroppingPhone;
    public float DropTime = 1.5f;
    public Transform PhoneStartPoint;
    public Transform PhoneEndPoint;
    public float DropTimeRemaining;
    private bool lastFacing;
    public GameObject PhoneOnGround;
    public GameObject PhoneBattery;
    private int barkCount;

    private void Start()
    {
        barkCount = 3;
        if (GameStateManager.Is("GuyWalkingDone"))
        {
            Destroy(gameObject);
        }
        else
        {
            ResetPause();
            Renderer = GetComponent<SpriteRenderer>();
            DropTimeRemaining = DropTime;
            IsDroppingPhone = false;
            lastFacing = Renderer.flipX;
        }
    }

    private void ResetPause()
    {
        IsPausing = false;
        TimeTilPause = Random.Range(2.0f, 5.0f);
        PauseTimeRemaining = Random.Range(0.5f, 2.5f);
    }

    private void Update()
    {
        Vector3 start;
        Vector3 end;
        if (IsGoingRight)
        {
            start = StartPoint.position;
            end = EndPoint.position;
        }
        else
        {
            start = EndPoint.position;
            end = StartPoint.position;
        }

        if (IsLeaving)
        {
            // How far would we move between points in this span of time
            float MoveSpeed = (Time.deltaTime * Speed * (EndPoint.position - StartPoint.position) / 100.0f).magnitude;
            // Move towards the leave point direction
            Vector3 UnitDir = (LeavePoint.position - transform.position).normalized;
            // Move at the above speed
            transform.position += UnitDir * MoveSpeed;
            Renderer.flipX = (LeavePoint.position.x < transform.position.x);

            // Did we get close enough to the end?
            if ((LeavePoint.position - transform.position).magnitude <= 0.1f)
            {
                // Close enough, done
                GameStateManager.Set("GuyWalkingDone");
                Destroy(gameObject);
            }
        }
        else if (IsDroppingPhone)
        {
            // Don't move, show phone dropping
            DropTimeRemaining -= Time.deltaTime;
            if (DropTimeRemaining < 0.0f)
            {
                DropTimeRemaining = 0.0f;
                IsDroppingPhone = false;
                IsLeaving = true;
            }
            Phone.transform.position = Vector3.Lerp(PhoneEndPoint.position, PhoneStartPoint.position, EasingFunction.EaseOutQuart(0, 1.0f, DropTimeRemaining/DropTime));
            if (IsDroppingPhone == false)
            {
                Vector3 Delta = PhoneBattery.transform.position - PhoneOnGround.transform.position;
                PhoneOnGround.transform.position = Phone.transform.position;
                PhoneOnGround.SetActive(true);
                PhoneBattery.transform.position = PhoneOnGround.transform.position + Delta;
                PhoneBattery.SetActive(true);
                Phone.SetActive(false);
            }
        }
        else
        {
            if (IsPausing)
            {
                PauseTimeRemaining -= Time.deltaTime;
                if (PauseTimeRemaining <= 0.0)
                {
                    ResetPause();
                }
            }
            else
            {
                Percent += Time.deltaTime * Speed / 100.0f;
                TimeTilPause -= Time.deltaTime;
                if (TimeTilPause <= 0.0)
                {
                    IsPausing = true;
                }
            }
            if (Percent >= 1.0f)
            {
                Percent = 1.0f;
            }
            transform.position = Vector3.Lerp(start, end, Percent);
            Renderer.flipX = !IsGoingRight;
            if (Percent >= 1.0f)
            {
                Percent = 0.0f;
                IsGoingRight = !IsGoingRight;
            }
        }

        if (lastFacing != Renderer.flipX)
        {
            lastFacing = Renderer.flipX;
            Phone.transform.localPosition = new Vector3(-Phone.transform.localPosition.x, Phone.transform.localPosition.y, Phone.transform.localPosition.z);
            PhoneStartPoint.transform.localPosition = new Vector3(-PhoneStartPoint.transform.localPosition.x, PhoneStartPoint.transform.localPosition.y, PhoneStartPoint.transform.localPosition.z);
            PhoneEndPoint.transform.localPosition = new Vector3(-PhoneEndPoint.transform.localPosition.x, PhoneEndPoint.transform.localPosition.y, PhoneEndPoint.transform.localPosition.z);
        }
    }

    public void BarkEvent(GameObject Barker)
    {
        Debug.Log("Guy walking Barked at by " + Barker);
        if ((!IsLeaving) && (!IsPausing))
        {
            bool IsDogToRight = Barker.transform.position.x > transform.position.x;

            // If different directions, the dog is behind us
            if (IsDogToRight != IsGoingRight)
            {
                // Takes three barks behind them to cause them to drop the phone
                barkCount--;
                if (barkCount == 0)
                {
                    IsDroppingPhone = true;
                }
            }
        }
    }
}
