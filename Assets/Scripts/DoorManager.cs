using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour
{
    public Item keyItem;

    private bool doorOpened = false;
    private Transform door;

    public float openDuration = 0.5f; // tiempo que tarda en abrir

    private void Start()
    {
        door = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorOpened) return;

        if (other.CompareTag("Player"))
        {
            if (InventoryManager.Instance.currentItems.Contains(keyItem))
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        doorOpened = true;

        Quaternion startRotation = door.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, -90, 0);

        float time = 0;

        while (time < openDuration)
        {
            door.rotation = Quaternion.Slerp(startRotation, targetRotation, time / openDuration);
            time += Time.deltaTime;
            yield return null;
        }

        door.rotation = targetRotation;

        Debug.Log("Door opened with key!");
    }
}