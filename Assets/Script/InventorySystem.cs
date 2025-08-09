using UnityEngine;
using System.Collections.Generic;
public class InventorySystem : MonoBehaviour
{
    public Item_Main_SO testwood;
    // start at 0
    public int maxInventorySlot;
    // Dictionnary containing object with a number of those, each of the object have an id inside the inventory
    // Key : Id, Pair : object, number
    //Dictionary<int, InventoryItem<Item_Main_SO, int>> inventory = new Dictionary<int, InventoryItem<Item_Main_SO, int>>();

    [SerializeField] List<Slot> slots;


    // TODO : case : 0, wood 54; 1, wood 32, new entry wood 20
    // -> 0, wood 64, 1, wood 38
    // Actually will just create another stack, now i want to add it to an another one

    void Start()
    {

        //AddItem(testwood, 120);

        /*
        Debug.Log("the inventory at start contain : " + inventory[0].Item + " " + inventory[0].Quantity);
        AddItem(testwood, 36);
        Debug.Log("the inventory now contain : " + inventory[0].Item + " " + inventory[0].Quantity);
        Debug.Log("the secondary slot in inventory now contain : " + inventory[1].Item + " " + inventory[1].Quantity);
        */
        /*
        AddItem("Wood", 60);
        AddItem("Steel", 14);
        AddItem("Wood", 14);

        Debug.Log(inventory[2].Item + " " + inventory[2].Quantity);
        */
    }


    /// <summary>
    /// Add item and a number in a inventory slot
    /// Check if item already present and increase stack (if overflow create a new one)
    /// If number value too big, split in different slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public void AddItem(Item_Main_SO item, int number)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty())
            {
                Debug.Log("Il y a un item dans ce slot");

                if (slots[i].item == item && slots[i].quantity != slots[i].item.maxItemInStack)
                {
                    Debug.Log("le slot contient cet item et n'est pas max");

                    int total = slots[i].quantity + number;
                    Debug.Log("total is : " + total);
                    // Add to the current stack
                    slots[i].quantity = Mathf.Clamp(total, 0, item.maxItemInStack);
                    UIManager.instance.AddItemToTheUI(item, slots[i].quantity, i);
                    for (int j = 0; j < slots.Count; j++)
                    {
                        if (slots[j].IsEmpty())
                        {
                            // If go past the limit of stack 
                            if (total > item.maxItemInStack)
                            {
                                // Update number and create a new stack of the item
                                number = total - item.maxItemInStack;
                                slots[j].UpdateSlot(item, number);
                                return;
                                //UIManager.instance.AddItemToTheUI(item, number, inventory.Count - 1);
                            }
                        }
                    }
                    return;
                }
            }
        }

        if(number > item.maxItemInStack)
        {
            int slotNeeded = Mathf.CeilToInt((float)number / (float)item.maxItemInStack);
            Debug.Log(slotNeeded);
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].item)
                {
                    int value = Mathf.Clamp(number, 0, 64);
                    slots[i].UpdateSlot(item, value);
                    number -= value;
                    Debug.Log(number);
                    slotNeeded--;
                    if(slotNeeded == 0)
                    {
                        return;
                    }
                }
            }
        }
        // Closest individual empty slot
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].item)
            {
                slots[i].UpdateSlot(item, number);
                return;
            }
        }


    }








    /*
    /// <summary>
    /// To add an item must : 
    /// Check if item not already in inventory, if so, add number to the current stack
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public void AddItem(Item_Main_SO item, int number)
    {
        // go through whole inventory
        foreach (var (key, value) in inventory)
        {
            // If picked up item is inside the inventory and not max stack
            if (value.Item == item && value.Quantity != value.Item.maxItemInStack)
            {
                int total = value.Quantity + number;
                // Add to the current stack
                value.Quantity = Mathf.Clamp(total, 0, item.maxItemInStack);
                UIManager.instance.AddItemToTheUI(item, value.Quantity, key);
                Debug.Log("key value : " + key);
                if (CheckIfSlotAvailable())
                {
                    // If go past the limit of stack 
                    if (total > item.maxItemInStack)
                    {
                        // Update number and create a new stack of the item
                        number = total - item.maxItemInStack;

                        inventory.Add(inventory.Count, new InventoryItem<Item_Main_SO, int>(item, number));
                        UIManager.instance.AddItemToTheUI(item, number, inventory.Count - 1);
                    }
                }
                return;
            }
        }
        // If new entry
        if (CheckIfSlotAvailable())
        {
            if (number > item.maxItemInStack)
            {
                int numberOfStack = Mathf.CeilToInt((float)number / (float)item.maxItemInStack);

                for (int i = 0; i < numberOfStack; i++)
                {
                    int quantity = Mathf.Clamp(number, 0, 64);
                    number -= quantity;
                    inventory.Add(inventory.Count, new InventoryItem<Item_Main_SO, int>(item, quantity));
                    UIManager.instance.AddItemToTheUI(item, quantity, inventory.Count - 1);
                }
            }
            else
            {
                inventory.Add(inventory.Count, new InventoryItem<Item_Main_SO, int>(item, number));
                UIManager.instance.AddItemToTheUI(item, number, inventory.Count - 1);
            }
        }
    }
    */

    bool CheckIfSlotAvailable()
    {
        return (slots.Count < maxInventorySlot) ? true : false;
    }

    /// <summary>
    /// Class custom std::pair for the inventory
    /// First is the item
    /// Second is the quantity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class InventoryItem<T, U>
    {
        // Empty Constructor
        public InventoryItem() { }

        // Init Constructor
        public InventoryItem(T item, U quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
        //Getter / Setter
        public T Item { get; set; }
        public U Quantity { get; set; }
    };
}

