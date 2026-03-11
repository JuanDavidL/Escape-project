using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShootForward : MonoBehaviour
{
    [SerializeField] float speedProjectile = 50f;

    Rigidbody _rb;

    void Start()
    {
        // A projectile debe tener un Rigidbody para recibir colisiones.
        _rb = GetComponent<Rigidbody>();
        /*if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.isKinematic = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }*/
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedProjectile * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        TryDestroyOnImpact(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && gameObject.CompareTag("Arrow") || other.CompareTag("Buildings") && gameObject.CompareTag("Arrow") || other.CompareTag("Player") && gameObject.CompareTag("Arrow"))
        {
            TryDestroyOnImpact(this.gameObject);
            Destroy(gameObject);
        }
    }

    void TryDestroyOnImpact(GameObject other)
    {
        // Se permiten etiquetas comunes en mayúsculas/minúsculas.
        string tag = other.tag;
        if (tag == "Ground" || tag == "ground" || tag == "Buildings" || tag == "buildings" || tag == "Player" || tag == "player")
        {
            Destroy(gameObject);
        }
    }
}
