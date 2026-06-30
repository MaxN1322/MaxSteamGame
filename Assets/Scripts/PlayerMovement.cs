using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private float acceleration = 20f;

    [SerializeField] private float deceleration = 18f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 720f;

    private Rigidbody2D rb;
    private PlayerControls controls;

    private Vector2 input;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>().normalized;
        controls.Player.Move.canceled += _ => input = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        // Desired velocity
        Vector2 targetVelocity = input * maxSpeed;

        // Accelerate or decelerate
        float currentAcceleration =
            input == Vector2.zero ? deceleration : acceleration;

        velocity = Vector2.MoveTowards(
            velocity,
            targetVelocity,
            currentAcceleration * Time.fixedDeltaTime);

        rb.linearVelocity = velocity;

        // Rotate toward movement direction
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;

            float newRotation = Mathf.MoveTowardsAngle(
                rb.rotation,
                angle,
                rotationSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(newRotation);
        }
    }
}