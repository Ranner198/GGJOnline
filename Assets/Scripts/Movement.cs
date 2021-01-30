using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{     
    public float speed;
    public Vector2 mousePosition;
    public new Camera camera;
    [HideInInspector]
    public Vector2 newPosition;
    public Animator anim;
    private float scaleSize = 0;
    private bool isMoving = false;
    private void Awake()
    {
        scaleSize = transform.localScale.x;
    }

    void Update()
    {
        Vector3 placeholder = Input.mousePosition;
        placeholder.z = 0;
        mousePosition = camera.ScreenToWorldPoint(placeholder);

        // Validate mouse position
        var view = camera.ScreenToViewportPoint(Input.mousePosition);

        Vector2 direction = newPosition - (Vector2)transform.position;
        var characterDir = transform.localScale;

        var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        if (!isOutside && Input.GetMouseButton(1))
        {
            newPosition = mousePosition;
            isMoving = true;
            if (direction.x > 1f) // Player Face Right
            {
                characterDir.x = scaleSize;
                anim.SetTrigger("Walk");
            }
            else if (direction.x < -1f) // Player Face Left
            {
                characterDir.x = -scaleSize;
                anim.SetTrigger("Walk");
            }
        }

        float distance = Vector2.Distance(transform.position, newPosition);
        transform.Translate((newPosition-(Vector2)transform.position).normalized * Time.deltaTime * speed);

        if (distance < 1 && isMoving == true)
        {
            isMoving = false;
            anim.SetTrigger("Idle");
        }

        // Face Direction
        transform.localScale = characterDir;
    }
}
