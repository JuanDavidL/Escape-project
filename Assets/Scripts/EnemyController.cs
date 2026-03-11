using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform detectionCenter;
    [SerializeField] float detectionRatio;
    [SerializeField] LayerMask detectionLayers;

    Collider[] _colisionesDeteccion;
    [SerializeField] bool _objetivo;
    [SerializeField] float _distanciaConObjetivo;
    [SerializeField] float _velocidadRotacionObjetivo;

    [SerializeField] Transform _navegacionCentro;
    [SerializeField] Vector3 _navegacionTamano;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Validamos todo colaider que entre en la zona de deteccion con la capa que hemos definido
        _colisionesDeteccion = Physics.OverlapSphere(detectionCenter.position, detectionRatio, detectionLayers);

        //Validamos que el personaje entre en el rango de deteccion del enemigo, si el valor es mayor a uno significa que el
        //Player esta en el rango
        _objetivo = _colisionesDeteccion.Length > 0 ? true : false;

        if (_objetivo)
        {
            //_distanciaConObjetivo = Vector3.Distance(transform.position, _colisionesDeteccion[0].transform.position);

            Vector3 _direccion = _colisionesDeteccion[0].transform.position - transform.position;
            //Vector3 _direccion = new Vector3(_colisionesDeteccion[0].transform.localPosition.x - 2, _colisionesDeteccion[0].transform.position.y, _colisionesDeteccion[0].transform.position.z) - transform.position;
            Quaternion _rotacion = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direccion), _velocidadRotacionObjetivo * Time.deltaTime);
            _rotacion.x = 0;
            _rotacion.z = 0;
            transform.rotation = _rotacion;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(detectionCenter.position, detectionRatio);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_navegacionCentro.position, _navegacionTamano);
    }
}
