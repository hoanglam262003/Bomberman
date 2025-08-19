using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedBoost,
    }

    public ItemType itemType;

    private void OnItemPickup(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBombs();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedBoost:
                player.GetComponent<MovementController>().speed ++;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
