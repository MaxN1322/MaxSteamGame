using UnityEngine;

public class LoopTeleporter : MonoBehaviour
{
    [Header("Movement Check")]
    [SerializeField] private float minimumMovement = 0.1f;

    [Header("Top -> Bottom")]
    [SerializeField] private float topTriggerY = 13.5f;
    [SerializeField] private float topDestinationY = -14f;
    [SerializeField] private float topDestinationXOffset = 0f;

    [Header("Bottom -> Top")]
    [SerializeField] private float bottomTriggerY = -14.5f;
    [SerializeField] private float bottomDestinationY = 13f;
    [SerializeField] private float bottomDestinationXOffset = 0f;

    private Rigidbody2D rb;

    public delegate void TeleportEvent(Vector3 offset);
    public static event TeleportEvent OnTeleport;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocity.magnitude < minimumMovement)
            return;

        Vector3 oldPos = transform.position;

        // Top Right -> Bottom Left
        if (transform.position.y >= topTriggerY && transform.position.x > 0)
        {
            transform.position = new Vector3(
                -transform.position.x + topDestinationXOffset,
                topDestinationY,
                transform.position.z);

            OnTeleport?.Invoke(transform.position - oldPos);
        }

        // Top Left -> Bottom Right
        else if (transform.position.y >= topTriggerY && transform.position.x < 0)
        {
            transform.position = new Vector3(
                -transform.position.x - topDestinationXOffset,
                topDestinationY,
                transform.position.z);

            OnTeleport?.Invoke(transform.position - oldPos);
        }

        // Bottom Right -> Top Left
        else if (transform.position.y <= bottomTriggerY && transform.position.x > 0)
        {
            transform.position = new Vector3(
                -transform.position.x + bottomDestinationXOffset,
                bottomDestinationY,
                transform.position.z);

            OnTeleport?.Invoke(transform.position - oldPos);
        }

        // Bottom Left -> Top Right
        else if (transform.position.y <= bottomTriggerY && transform.position.x < 0)
        {
            transform.position = new Vector3(
                -transform.position.x - bottomDestinationXOffset,
                bottomDestinationY,
                transform.position.z);

            OnTeleport?.Invoke(transform.position - oldPos);
        }
    }
}