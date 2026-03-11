using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Buscamos el componente de salud en lo que sea que hayamos tocado
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}