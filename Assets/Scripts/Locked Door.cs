using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerInventory inv = collision.gameObject.GetComponent<PlayerInventory>();

        if (inv != null && inv.hasKey)
        {
            Destroy(gameObject);
        }
    }
}