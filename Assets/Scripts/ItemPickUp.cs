using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private PowerUpData data;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Se obtiene el script de estadísticas
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.ApplyPowerUp(data);
                // Comprobamos tipo de powerup
                Debug.Log($"Power-up recogido: {data.targetStat}");
                Destroy(gameObject);
            }
        }
    }
}