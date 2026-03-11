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
        if (other.tag == "Player"){
            Debug.Log("got t o trigger enter");
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
