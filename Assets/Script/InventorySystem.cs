using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public PickableItem testwood;
    public GameObject InventoryPanel;
    [SerializeField] List<Slot> slots;


    /// <summary>
    /// Add item and a number in a inventory slot
    /// Check if item already present and increase stack (if overflow create a new one)
    /// If number value too big, split in different slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public void AddItem(PickableItem pickableItem, int number)
    {
        // Retrieve possible slot
        List<Slot> possibleSlots = FindPossiblesSlots(pickableItem.item);
        if (possibleSlots.Count == 0)
        {
            Debug.Log("All slots are full");
            return;
        }
        // Fill the possible slots 
        DistributeQuantityOverSlots(possibleSlots, number, pickableItem);
        pickableItem.PlayInteractionSound();

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

    /// <summary>
    /// Create a list of all slot that can receive the item (empty and one having the item and not full)
    /// Priority to those with the item inside so it fill those slots earlier
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    List<Slot> FindPossiblesSlots(Item_Main_SO itemToAdd)
    {
        List<Slot> emptySlot = new List<Slot>();
        List<Slot> futurSlot = new List<Slot>();
        for (int i = 0; i < slots.Count; i++)
        {
            // Contain the object
            if (slots[i].item == itemToAdd && !slots[i].IsFull())
            {
                futurSlot.Add(slots[i]);
            }
            else if (slots[i].IsEmpty())
            {
                emptySlot.Add(slots[i]);
            }
        }
        futurSlot.AddRange(emptySlot);
        /*
        foreach (Slot elem in futurSlot)
        {
            Debug.Log(elem);
        }
        */
        return futurSlot;
    }

    public void OnInventoryClose()
    {
        foreach(Slot slot in slots)
        {
            slot.ResetPostDrag();
        }
    }


}

