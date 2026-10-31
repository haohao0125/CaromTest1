using UnityEngine;

public class MoveAfterAWhile : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition; // Where it slides to
    [SerializeField] private float slideSpeed = 10f;
    [SerializeField] private float startDelay = 0.5f;

    private bool canSlide = false;

    private void Start()
    {
        gameObject.SetActive(true);
        Invoke(nameof(StartSliding), startDelay);
    }

    void StartSliding()
    {
        canSlide = true;
    }

    void Update()
    {
        if (!canSlide) return;

        // MoveTowards provides a perfectly constant, jitter-free speed
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            slideSpeed * Time.deltaTime
        );

        // Once it reaches the exact target position, destroy itself
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            Destroy(gameObject);
        }
    }
}