using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smoothTime = 0.18f;

    [SerializeField] private Vector3 offset = new Vector3(0,0,-10);

    private Vector3 velocity;

    private void OnEnable()
    {
        LoopTeleporter.OnTeleport += HandleTeleport;
    }

    private void OnDisable()
    {
        LoopTeleporter.OnTeleport -= HandleTeleport;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desired = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desired,
            ref velocity,
            smoothTime);
    }

    void HandleTeleport(Vector3 delta)
    {
        transform.position += delta;
    }
}