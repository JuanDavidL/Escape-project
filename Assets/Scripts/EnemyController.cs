using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] Transform detectionCenter;
    [SerializeField] float detectionRatio;
    [SerializeField] LayerMask detectionLayers;

    [Header("Aim")]
    [SerializeField] float velocityRotateToObjective = 3f;
    [SerializeField] float maxAimAngle = 20f;

    [Header("Shooting")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootInterval = 3f;

    [Header("Debug / Navigation")]
    [SerializeField] Transform navigateCenter;
    [SerializeField] Vector3 navigationSize;

    Collider[] collisionsDetection;
    bool objective;

    float _shootTimer;
    Quaternion _initialLocalRotation;

    void Start()
    {
        _initialLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Validamos todo collider que entre en la zona de detección con la capa que hemos definido
        collisionsDetection = Physics.OverlapSphere(detectionCenter.position, detectionRatio, detectionLayers);

        bool wasObjective = objective;
        objective = collisionsDetection.Length > 0;

        if (objective)
        {
            // Si acabamos de detectar al jugador, forzamos el primer disparo inmediato
            if (!wasObjective)
                _shootTimer = shootInterval;

            Transform target = collisionsDetection[0].transform;
            Vector3 worldDirection = (target.position - transform.position).normalized;
            if (worldDirection.sqrMagnitude > 0.001f)
            {
                // Convertimos la dirección al espacio local del padre para que el control se efectúe en local
                Vector3 localDirection = transform.parent != null ? transform.parent.InverseTransformDirection(worldDirection) : worldDirection;

                Quaternion targetLocalRotation = Quaternion.LookRotation(localDirection);
                Vector3 targetEuler = targetLocalRotation.eulerAngles;

                Vector3 initialEuler = _initialLocalRotation.eulerAngles;
                float yaw = ClampAngleDelta(targetEuler.y - initialEuler.y, maxAimAngle);
                float pitch = ClampAngleDelta(targetEuler.x - initialEuler.x, maxAimAngle);

                Vector3 clampedEuler = new Vector3(initialEuler.x + pitch, initialEuler.y + yaw, initialEuler.z);
                Quaternion desiredLocalRotation = Quaternion.Euler(clampedEuler);

                transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredLocalRotation, velocityRotateToObjective * Time.deltaTime);
            }

            HandleShooting();
        }
        else
        {
            _shootTimer = 0f;
        }
    }

    void HandleShooting()
    {
        if (arrowPrefab == null || firePoint == null)
            return;

        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootInterval)
        {
            _shootTimer = 0f;
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        }
    }

    static float ClampAngleDelta(float delta, float maxDelta)
    {
        delta = NormalizeAngle(delta);
        return Mathf.Clamp(delta, -maxDelta, maxDelta);
    }

    static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        if (angle < -180f) angle += 360f;
        return angle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(detectionCenter.position, detectionRatio);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(navigateCenter.position, navigationSize);
    }
}
