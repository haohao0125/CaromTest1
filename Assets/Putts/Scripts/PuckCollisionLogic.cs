using UnityEngine;

public class PuckCollisionLogic : MonoBehaviour
{
    [SerializeField] private float magShake = 0.02f;
    [SerializeField] private float magClamp = 0.3f;

    private void OnCollisionEnter2D(Collision2D collision) {

        float impactVelocity = collision.relativeVelocity.magnitude;

        // shake impact threshold 
        if (impactVelocity > 0.8f)
        {
            float shakeMag = impactVelocity * magShake;
            float clampedMag = Mathf.Min(shakeMag, magClamp); // Max limit

            CameraShake.Shake(0.15f, clampedMag);
        }
    }
}
