﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{     
    public float speed;
    public Vector2 mousePosition;
    public new Camera camera;
    [HideInInspector]
    public Vector2 newPosition;
    public Animator anim;
    private float scaleSize = 0;
    public bool isMoving = false;
    public string lastSceneName;
    public EventSystem eventSystem;
    public static Movement instance;
    private bool cancelMoving = false;
    public AudioSource BarkSound;

    private void Awake()
    {
        scaleSize = transform.localScale.x;
        instance = this;
    }

    private void Start()
    {
        camera = Camera.main;
        SpawnPosition StartPositions = FindObjectOfType<SpawnPosition>();
        newPosition = transform.position = StartPositions.Spawned(this.transform, GameStateManager.instance.lastSceneName);
        //Debug.Log("Loaded from scene: " + GameStateManager.instance.lastSceneName + " and starting at " + newPosition);

        newPosition = transform.position;
        if (anim != null) anim.StopPlayback();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        bool changeFacing = true;
        bool moveToPoint = true;
        Vector3 placeholder = Input.mousePosition;
        placeholder.z = 0;
        mousePosition = camera.ScreenToWorldPoint(placeholder);

        // Validate mouse position
        var view = camera.ScreenToViewportPoint(Input.mousePosition);

        Vector2 direction = newPosition - (Vector2)transform.position;
        var characterDir = transform.localScale;

        var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        if (!isOutside && (Input.GetMouseButtonDown(0) && !cancelMoving))
        {
            bool found = false;

            if (Overlay.instance.CurrentPopup == "")
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition, Vector2.zero, 100);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.tag == "Interactable")
                    {
                        found = true;
                        return;
                    }
                    if (hit[i].transform.tag == "Player")
                    {
                        // Clicked on dog!
                        DoBark();
                        changeFacing = false;
                        moveToPoint = false;
                    }
                }
            }
            else
            {
                // Overlay blocks any movement
                moveToPoint = false;
            }

            if (found)
                return;

            if (moveToPoint)
            {
                newPosition = mousePosition;
                direction = newPosition - (Vector2)transform.position;
                isMoving = true;
                if (changeFacing)
                {
                    if (direction.x > 0f) // Player Face Right
                    {
                        characterDir.x = Mathf.Abs(scaleSize);
                    }
                    else if (direction.x < 0f) // Player Face Left
                    {
                        characterDir.x = -Mathf.Abs(scaleSize);
                    }
                }
            }
        }
        cancelMoving = false;

        if (moveToPoint)
        {
            float distance = Vector2.Distance(transform.position, newPosition);
            if (distance > 0.1f)
            {
                transform.Translate((newPosition - (Vector2)transform.position).normalized * Time.deltaTime * speed);
                if (anim != null) anim.SetFloat("Vel", distance);
            }
            else
            {
                if (anim != null) anim.SetFloat("Vel", 0.0f);
            }
        }
        else
        {
            if (anim != null) anim.SetFloat("Vel", 0.0f);
        }

        // Face Direction
        transform.localScale = characterDir;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPosition StartPositions = FindObjectOfType<SpawnPosition>();
        Debug.Log("Loaded scene!");

        if (StartPositions)
        {
            newPosition = transform.position = StartPositions.Spawned(this.transform, GameStateManager.instance.lastSceneName);
            if (anim != null) anim.StopPlayback();
            Debug.Log("Loaded from scene: " + GameStateManager.instance.lastSceneName + " and starting at " + newPosition);
        }
        else
        {
            Debug.LogError("Didn't find list of spawns! Going to (0,0)");
            // Do nothing
        }
    }

    public void HaltMovement()
    {
        //Debug.Log("Halting movement");
        newPosition = transform.position;
        if (anim != null) anim.StopPlayback();
        isMoving = false;
        cancelMoving = true;
    }

    public void DoBark()
    {
        Debug.Log("Bark!!");
        anim.SetTrigger("Bark");
        BarkSound.Play();
        ListenForBark[] Listeners = FindObjectsOfType<ListenForBark>();
        foreach (ListenForBark Listener in Listeners)
        {
            Listener.BarkEvent.Invoke(gameObject);
        }
    }
}
