using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();
    public PickableItem testwood;
    [SerializeField] int nbrOfSlots;
    [SerializeField] int maxSlotsInRow; // Nbr of element in a row
    [SerializeField] int spacingX;
    [SerializeField] int spacingY;
    public GameObject InventoryPanel;
    [SerializeField] Slot slotToSpawn;
    public List<Slot> slots;
    [SerializeField] List<Slot> potentialSlots;
    [SerializeField] float dragOutForce = 5f; // When object is throw away

    private void Start()
    {
        InitializeInventorySlot();
        GameEvents.OnObjectThrow += ThrowItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown("3"))
        {
            GetAllItem();
        }
    }

    private void ThrowItem(Item_Main_SO item)
    {
            Debug.Log(gameObject.name);
            PickableItem ItemDropped = Instantiate(item.DropModel, gameObject.transform.position, Quaternion.identity);
            ItemDropped.Initialize(item, 1, item.DropModel.affectOnlyPlayer, item.DropModel.itemType);

            ItemDropped.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * dragOutForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Add item and a number in a inventory slot
    /// Check if item already present and increase stack (if overflow create a new one)
    /// If number value too big, split in different slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public void AddItem(PickableItem pickableItem, int number)
    {
        if (!FindPossiblesSlots(pickableItem.item))
        {
            return;
        }
        // Fill the possible slots 
        DistributeQuantityOverSlots(potentialSlots, number, pickableItem);
        pickableItem.PlayInteractionSound();
    }

    public void AddItem(Item_Main_SO craftedItem, int number)
    {
        if (!FindPossiblesSlots(craftedItem))
        {
            return;
        }
        // Fill the possible slots 
        DistributeQuantityOverSlots(potentialSlots, number, craftedItem);
    }

    /// <summary>
    /// Distribute the value into one or multiple slots
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="pickableItem"></param>
    /// <param name="idx"></param>
    void DistributeQuantityOverSlots(List<Slot> possibleSlots, int quantity, PickableItem pickableItem)
    {
        // From the one with item !full tot the empty
        for (int i = 0; i < possibleSlots.Count; i++)
        {
            int currentQuantity = possibleSlots[i].quantity;
            int maxStack = pickableItem.item.maxItemInStack;
            int spaceLeft = maxStack - currentQuantity;
            int addValue = Mathf.Min(quantity, spaceLeft); // Retrieve the value to substract

            possibleSlots[i].UpdateSlot(pickableItem.item, currentQuantity + addValue);
            quantity -= addValue;
            if (quantity <= 0)
            {
                pickableItem.GetTheRest(quantity);
                return;
            }
        }
        pickableItem.GetTheRest(quantity);
    }


    void DistributeQuantityOverSlots(List<Slot> possibleSlots, int quantity, Item_Main_SO craftedItem)
    {
        // From the one with item !full tot the empty
        for (int i = 0; i < possibleSlots.Count; i++)
        {
            int currentQuantity = possibleSlots[i].quantity;
            int maxStack = craftedItem.maxItemInStack;
            int spaceLeft = maxStack - currentQuantity;
            int addValue = Mathf.Min(quantity, spaceLeft); // Retrieve the value to substract

            possibleSlots[i].UpdateSlot(craftedItem, currentQuantity + addValue);
            quantity -= addValue;
        }
    }

    /// <summary>
    /// Create a list of all slot that can receive the item (empty and one having the item and not full)
    /// Priority to those with the item inside so it fill those slots earlier
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    public bool FindPossiblesSlots(Item_Main_SO itemToAdd)
    {
        potentialSlots = new List<Slot>();
        List<Slot> emptySlot = new List<Slot>();
        for (int i = 0; i < slots.Count; i++)
        {
            // Contain the object
            if (slots[i].item == itemToAdd && !slots[i].IsFull())
            {
                potentialSlots.Add(slots[i]);
            }
            else if (slots[i].IsEmpty())
            {
                emptySlot.Add(slots[i]);
            }
        }
        potentialSlots.AddRange(emptySlot);
        Debug.Log("potential count avaialable "  + potentialSlots.Count);
        return potentialSlots.Count > 0 ? true : false;
    }

    public void OnInventoryClose()
    {
        foreach (Slot slot in slots)
        {
            slot.ResetPostDrag();
        }
    }


    public void InitializeInventorySlot()
    {
        int slotInCurrentRow = 0;
        if (slots.Count == 0)
        {
            for(int i = 0; i < nbrOfSlots; i++)
            {
                slotInCurrentRow++;
                if(slotInCurrentRow > maxSlotsInRow-1)
                {
                    slotInCurrentRow = 0;
                    spacingX = -1500;
                    spacingY -= 200;
                }

                Vector2 pos = new Vector2(spacingX, spacingY);
                Slot newSlot = Instantiate(slotToSpawn, InventoryPanel.transform);
                newSlot.GetComponent<Image>().rectTransform.anchoredPosition = pos;
                slots.Add(newSlot);
                spacingX += 1000;

            }

        }
    }

    public List<Ingredient> GetAllItem()
    {
        ingredients = new List<Ingredient>();
        bool isInside = false; // Flag
        // Check in every inventory slot
        foreach (Slot slot in slots)
        {
            if (slot.IsEmpty())
            {
                continue;
            }
            Ingredient ingredientToAdd = new Ingredient(slot.item, slot.quantity);
            // Look in every ingredient the player currently have
            foreach (Ingredient ingredient in ingredients)
            {
                // Check if item in list already
                if (slot.CompareItem(ingredient.item))
                {
                    ingredient.count += slot.quantity;
                    isInside = true;
                }
                else
                {
                    isInside = false;
                }
            }
            // New ingredient 
            if (!isInside)
            {
                ingredients.Add(ingredientToAdd);
                //Debug.Log("ingredientToAdd : " + ingredientToAdd.item);
                //Debug.Log("ingredientToAdd : " + ingredientToAdd.count);
            }
        }
        return ingredients;
        /*Debug.Log("ing count = " + ingredients.Count);
        foreach(Ingredient ing in ingredients)
        {
            Debug.Log(ing.item);
            Debug.Log(ing.count);
        }
        */
    }
}

