using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the player has the correct tag
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.IncrementTotalJumps();  // Add coins to the player's total
            }
            Destroy(gameObject);  // Remove the coin after collection
        }
    }
}