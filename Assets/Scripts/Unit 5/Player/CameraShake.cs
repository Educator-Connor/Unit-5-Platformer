using UnityEngine;
using System.Collections;

/// <summary>
/// This script adds some additional feedback to the player when they collide with the enemy or hazards
/// </summary>
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        originalPosition = transform.localPosition;
    }


    // Having a method first ensures that should the player be in a position that they would call this multiple times,
    // we can stop the coroutines.
    public void Shake(float duration, float magnitude)
    {
        StopAllCoroutines();
        originalPosition = transform.localPosition;
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }
    
    // Shake coroutine that loops for the duration 
    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
