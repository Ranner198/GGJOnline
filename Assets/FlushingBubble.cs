using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushingBubble : MonoBehaviour
{
    SpriteRenderer Renderer;

    public float OnMin = 1.0f;
    public float OnMax = 2.0f;
    public float OffMin = 0.5f;
    public float OffMax = 0.5f;
    public string StateRequired;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();

        if (GameStateManager.Is(StateRequired))
        {
            StartCoroutine(Flashing());
        }
        else
        {
            // We don't exist until we have that state
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    IEnumerator Flashing()
    {
        yield return 0;

        while (true)
        {
            Renderer.enabled = true;
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
            Renderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.5f, 0.5f));
        }
    }
}
