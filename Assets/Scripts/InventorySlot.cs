using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item currentItem;
    public GameObject playerRef;
    private InventoryUI inventoryUI;

    private InventoryManager inventoryManager;
     
    //private InventoryManager inventoryManager;

    //public Item currentItem;

    private void Start()
    {
        //currentItem = null;
        icon = GetComponent<Image>();
        inventoryUI = GetComponentInParent<InventoryUI>();
        playerRef = GameObject.Find("Player");
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
    }

    public void SetSlot(Item item)
    {
        currentItem = item;
        icon.enabled = true;
        icon.sprite = item._icon;
    }

    public void ThrowItem()
    {
        if (currentItem != null)
        {
            Debug.Log("This slot had an object, which was" + currentItem._itemName);
            inventoryManager.RemoveItem(currentItem);
            Instantiate (currentItem._prefab, new Vector3(playerRef.transform.position.x,playerRef.transform.position.y-2f,playerRef.transform.position.z), Quaternion.identity);
            
            
        }

        else Debug.Log("This slot was empty, so nothing happened");

        ClearSlot();
        
    }


    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    [ContextMenu("Destroy this sloth")]
    public void DestroyThisSlot()
    {
        if (currentItem != null) ThrowItem();
        inventoryUI.RemoveSpecificSlot(transform.parent.gameObject);
    }


}
