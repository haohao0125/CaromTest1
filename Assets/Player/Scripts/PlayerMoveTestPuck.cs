using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPuck : MonoBehaviour
{
    private InputAction moveAction;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        MoveScript();
    }


    private void MoveScript() {

        if (moveAction != null)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();

            //Vector2 moveDirection = new Vector2(input.x, input.y);s
            //rb.linearVelocity = moveDirection * moveSpeed;

            Vector2 moveForce = input * moveSpeed;

            rb.AddForce(moveForce);
        }
    }

}
