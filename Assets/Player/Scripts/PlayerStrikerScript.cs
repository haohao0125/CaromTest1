using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerStrikerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    public bool canDrag;
    private bool isDragging = false;


    private Vector2 mousePressPos;

    [Header("Flick Settings")]
    [SerializeField] private float powerMultiplier = 5f;
    [SerializeField] private float maxDragDistance = 3f;

    private PlayerMoveTestPuck movePosPuck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;


    }

    void Update()
    {
        if (canDrag) {
            handleDragingPlay();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Puck")) {
            canDrag = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Puck")) {
            canDrag = true;
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
                
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentMousePos);

            movePosPuck = FindFirstObjectByType<PlayerMoveTestPuck>();
            movePosPuck.canMove = false;


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
                canDrag = false;

                // switch from trigger to collider
                Collider2D col = GetComponent<Collider2D>();
                if (col != null) {
                    col.isTrigger = false;
                }

            }
        }
    }


    

}