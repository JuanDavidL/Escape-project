using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> currentItems =  new List<Item>();

    public UnityEvent OnItemAdded, OnItemRemoved, OnInventoryGrowth, OnInventoryShrink;

    [Header("Inventory options")]
    public InventoryUI inventoryUI;
    //private bool isInventoryActive = false;
    public static int currentInventorySize = 2;
    public int maxInventorySize = 4;
    public Item testingItem;

    //public Transform itemContent;
    //public GameObject InventoryItem;

    //SistemaVida vida;
    //weaponAmmo Municion;
    
    //SpriteHolder panelinventory;
   


    private void Start()
    {
        currentInventorySize = 2;
        //inventoryUI.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        //vida = FindAnyObjectByType<SistemaVida>();
        //Municion = FindAnyObjectByType<weaponAmmo>();
    }

    public void TestFunction()
    {
        AddItem(testingItem);
    }

    public bool AddItem(Item itemToAdd)
    {
        if (currentItems.Count < currentInventorySize)
        {
            currentItems.Add(itemToAdd);

            GameObject availableSpot = inventoryUI.GetAvailableSlot();
            availableSpot.GetComponent<InventorySlot>().SetSlot(itemToAdd);

            OnItemAdded.Invoke();
            return true;
            //if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        }
        else
        {
            Debug.Log("Inventory is full");
            return false;
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        Debug.Log("It got to removeitem function in inventory manager");
        if(currentItems.Count > 0)
        {
            currentItems.Remove(itemToRemove);
            //OnItemRemoved.Invoke();
            Debug.Log("Current inventory fileld size is" + currentItems.Count);
        }
        else
        {
            Debug.Log("Can't remove item, as it is already empty");
        }
    }

    public void GrowInventory()
    {
        //Debug.Log("Current inventory size is " + currentInventorySize + ". Will attemp to grow");
        if (currentInventorySize < maxInventorySize)
        {
            currentInventorySize++;
            //Debug.Log("Grow was succesful, new size is " + currentInventorySize);
            //OnInventoryGrowth.Invoke();
        } 
        else 
        {
            Debug.Log("Inventory is already max size");
        }
    }

    public void ShrinkInventory()
    {
        //Debug.Log("Current inventory size is " + currentInventorySize + ". Will attemp to shrink");
        if (currentInventorySize > 1)
        {
            currentInventorySize--;
            //Debug.Log("Shrink was succesful, new size is " + currentInventorySize);
            //OnInventoryShrink.Invoke();
        } 
        else Debug.Log("Inventory is already minimum size, that is " + currentInventorySize);
    }


// CALL EVENTS TESTING

    [ContextMenu("Invoke OnItemAdded")]
    public void InvokeOnAdd()
    {
        OnItemAdded.Invoke();
    } 

    [ContextMenu("Invoke OnItemRemoved")]
    public void InvokeOnRemove()
    {
        OnItemRemoved.Invoke();
    }

    [ContextMenu("Invoke OnInventoryGrowth")]
    public void InvokeOnGrowth()
    {
        OnInventoryGrowth.Invoke();
    }

    [ContextMenu("Invoke OnInventoryShrink")]
    public void InvokeOnShrink()
    {
        OnInventoryShrink.Invoke();
    }
    
}
