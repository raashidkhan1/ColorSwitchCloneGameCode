using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public RectTransform imageRectTransform; // Reference to the RectTransform of the image
    public float scaleFactor = 1.2f; // The amount by which to grow the image (relative to its initial scale)
    public float duration = 0.5f; // The time it takes to grow and shrink

    private Vector3 initialScale;

    void Start()
    {
        // Store the initial scale of the image
        initialScale = imageRectTransform.localScale;

        // Start the grow and shrink effect
        StartCoroutine(GrowShrink());
        
        //dont rotate the stars
        imageRectTransform.rotation = Quaternion.identity;
    }

    // Coroutine to grow and shrink the star image
    IEnumerator GrowShrink()
    {
        Vector3 targetScale = initialScale * scaleFactor;

        while (true)
        {
            // Grow the image
            yield return StartCoroutine(ScaleImage(initialScale, targetScale, duration));

            // Shrink the image back to original size
            yield return StartCoroutine(ScaleImage(targetScale, initialScale, duration));
            
        }
    }

    // Coroutine to scale the image
    IEnumerator ScaleImage(Vector3 fromScale, Vector3 toScale, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            imageRectTransform.localScale = Vector3.Lerp(fromScale, toScale, elapsed / duration);
            yield return null;
        }

        imageRectTransform.localScale = toScale;
    }
}
