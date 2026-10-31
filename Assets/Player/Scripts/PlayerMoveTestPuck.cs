using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTestPuck : MonoBehaviour
{
    private InputAction moveAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float addForce = 1f;

    private bool canTwerk = false;
    public bool canMove = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void OnEnable()
    {
        canMove = true;
    }


    void Update()
    {
        AddForceScript();
        MoveAction();
    }


    private void MoveAction() {

        if (moveAction != null && canMove) {
            Vector2 input = moveAction.ReadValue<Vector2>();
            input.y = 0f;

            Vector2 moveDirection = new Vector2(input.x, input.y);
            rb.linearVelocity = moveDirection * moveSpeed;


            // we clampin
            float minX = -2.3f;
            float maxX = 2.3f;

            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        }

    }


    private void AddForceScript() {

        if (moveAction != null && canTwerk)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            input.y = 0f;

            Vector2 moveForce = input * addForce;

            rb.AddForce(moveForce);
        }
    }

}
