using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class FlickPuck : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    private bool canDrag;
    private bool isDragging = false;

    public UnityEvent onMouseReleased;
    private Vector2 mousePressPos;

    [Header("Flick Settings")]
    [SerializeField] private float powerMultiplier = 5f;
    [SerializeField] private float maxDragDistance = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;


    }

    void Update()
    {
        handlePlayer1();

        if (canDrag) {
            handleDragingPlay();
        }
    }



    private void handlePlayer1() {
        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();

        if (handler.Player1Turn)
        {
            canDrag = true;
        }
        else if (handler.Player2Turn)
        {
            canDrag = false;
        }
    }

    
    private void handleDragingPlay() {
        // Make sure a mouse is connected
        if (Mouse.current == null) return;

        // When left mouse button is pressed down
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // Check if we clicked directly on this puck's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPos);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
                mousePressPos = mouseWorldPos;
                lineRenderer.enabled = true;
            }
        }




        // While holding and dragging the mouse
        if (isDragging)
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                
            // Draw line from puck position to current mouse drag position
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentMousePos);

            // When you release the mouse button
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                lineRenderer.enabled = false;

                // Calculate pull vector (pull back to shoot forward)
                Vector2 pullVector = mousePressPos - currentMousePos;

                if (pullVector.magnitude > maxDragDistance)
                {
                    pullVector = pullVector.normalized * maxDragDistance;
                }

                // Shoot the puck using physics impulse
                rb.AddForce(pullVector * powerMultiplier, ForceMode2D.Impulse);

                // find the handler to review movement check of all pucks after release
                FindFirstObjectByType<GameHandlerScript>()?.StartMovementCheck();

                onMouseReleased.Invoke();
            }
        }
    }

}