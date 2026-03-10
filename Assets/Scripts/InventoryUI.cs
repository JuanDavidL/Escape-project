using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private float baseWidth, currentWidth;
    
    public float growthStepSize = 50f;
    public int totalSlots;

    public GameObject slotPrefab;

    private InventoryManager inventoryManager;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        baseWidth = rectTransform.sizeDelta.x;
        currentWidth = baseWidth;
        growthStepSize = slotPrefab.GetComponent<RectTransform>().sizeDelta.x;
        
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
        totalSlots = InventoryManager.currentInventorySize;

        
        for (int i = 0; i < totalSlots; i++)
        {
            AddSlot();
        }

    }

    [ContextMenu("Add slot")]
    public void AddSlot()
    {
        //Debug.Log("AddSlot was called once in InventoryUI script");
        GrowContainer();
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(gameObject.transform, false);
    }

    [ContextMenu("Remove slot")]
    public void RemoveLastSlot()
    {        
        //Debug.Log("RemoveSlot was called once in InventoryUI script");    
        GetLastSloth().DestroyThisSlot();
    }

    
    public void GrowContainer()
    {
        Debug.Log("Initial width was " + currentWidth + ". Growing now");
        currentWidth += growthStepSize;
        rectTransform.sizeDelta = new Vector2(currentWidth, rectTransform.sizeDelta.y);
        Debug.Log("New width is " + currentWidth);
    }

    
    public void ShrinkContainer()
    {
        Debug.Log("Initial width was " + currentWidth + ". Shrinking now");
        currentWidth -= growthStepSize;
        rectTransform.sizeDelta = new Vector2(currentWidth, rectTransform.sizeDelta.y);
        Debug.Log("New width is " + currentWidth);
    }

    

    public void RemoveSpecificSlot(GameObject slotToRemove)
    {        
        ShrinkContainer();
        Destroy(slotToRemove);
    }

    public GameObject GetAvailableSlot()
    {
        
        foreach (Transform child in transform)
        {
            InventorySlot closestEmptySlot = child.GetComponentInChildren<InventorySlot>();
           
            if (closestEmptySlot.currentItem == null)
            {
                 Debug.Log("The closest empty sloth was " + closestEmptySlot.gameObject.name);
                return closestEmptySlot.gameObject;
            }

        }

        Debug.Log("No slots were available when GetAvailableSloth was called");
        return null;
    }

    public InventorySlot GetLastSloth()
    {
        InventorySlot[] allSloths = gameObject.GetComponentsInChildren<InventorySlot>();
        
        if (allSloths != null)
        {
            Debug.Log("Last sloth found was " + allSloths[allSloths.Length-1].gameObject.transform.parent.name);
            return allSloths[allSloths.Length-1];
        }
        else
        {
            Debug.Log("There was not a single sloth avalilable. That shouldn't be possible???");
            return null;
        }
        
    }

}
