using UnityEngine;
using UnityEngine.Events;

public class ItemCollection : MonoBehaviour
{
   
    public Item itemRef;
    private InventoryManager inventoryManager;

    private void Start() {
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();

    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player"){
            Debug.Log("got to trigger enter");
            if (inventoryManager.AddItem(itemRef)) 
            {
                DestroyCollectible();               
            }
        }
    }

    void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
