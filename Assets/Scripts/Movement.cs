using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{     
    public float speed;
    public Vector2 mousePosition;
    public new Camera camera;
    private Vector2 newPosition;

    void Update()
    {
        Vector3 placeholder = Input.mousePosition;
        placeholder.z = 0;
        mousePosition = camera.ScreenToWorldPoint(placeholder);

        // Validate mouse position
        var view = camera.ScreenToViewportPoint(Input.mousePosition);

        var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        if (!isOutside && Input.GetMouseButton(1))
        {
            newPosition = mousePosition;
        }

        float distance = Vector2.Distance(transform.position, newPosition);
        float finalSpeed = (distance / speed) * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime / finalSpeed);
    }
}
