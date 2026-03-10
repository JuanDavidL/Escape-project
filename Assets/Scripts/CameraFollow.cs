using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 3f, -6f); 
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        // Posición deseada detrás del jugador
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Movimiento suave hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // En lugar de LookAt directo, mantenemos la cámara alineada con la rotación del jugador
        Quaternion targetRotation = Quaternion.Euler(0f, player.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }
}
