using Unity.Mathematics;
using UnityEngine;

public class itemMove : MonoBehaviour
{
    private Vector3 positionInit;
    [SerializeField] private float heightMove = 0.25f;
    [SerializeField] private float speedMove = 2f;
    [SerializeField] private float rotationSpeed = 100f;

    void Start()
    {
        positionInit = transform.position;
    }

    void Update()
    {
        float offsetVertical = Mathf.Sin(Time.time * speedMove) * heightMove;
        transform.position = new Vector3(positionInit.x, positionInit.y + offsetVertical, positionInit.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }
}
