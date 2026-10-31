using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;
    private Vector3 originalPos;
    private Coroutine shakeCoroutine;

    private void Awake() {
        instance = this;
        // Cache the true center position once when the game starts
        originalPos = transform.localPosition;
    }

    
    public static void Shake(float duration, float magnitude) {
        if (instance != null) {

            if (instance.shakeCoroutine != null) {
                instance.StopCoroutine(instance.shakeCoroutine);
                instance.transform.localPosition = instance.originalPos;
            }

            instance.shakeCoroutine = instance.StartCoroutine(instance.ShakeRoutine(duration, magnitude));
        }
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude){
       
        float elapsed = 0.0f;

        while (elapsed < duration) {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        shakeCoroutine = null;
    }
}