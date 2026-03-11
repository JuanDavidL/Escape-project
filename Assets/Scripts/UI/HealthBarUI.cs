using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    private Image healthBarImage;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        // 1. Nos suscribimos al evento cuando el script se activa
        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // 2. IMPORTANTE: Siempre desuscribirse para evitar fugas de memoria
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        // 3. Simplemente asignamos el valor (0 a 1)
        healthBarImage.fillAmount = healthPercentage;
    }
}