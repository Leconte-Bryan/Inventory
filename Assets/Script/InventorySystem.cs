using UnityEngine;
using System.Collections.Generic;
public class InventorySystem : MonoBehaviour
{
    int maxItemInStack = 64;
    // Dictionnary containing object with a number of those, each of the object have an id inside the inventory
    // Key : Id, Pair : object, number
    Dictionary<int, InventoryItem<string, int>> inventory = new Dictionary<int, InventoryItem<string, int>>();


    // TODO : case : 0, wood 54; 1, wood 32, new entry wood 20
    // -> 0, wood 64, 1, wood 38
    // Actually will just create another stack, now i want to add it to an another one

    void Start()
    {
        AddItem("Wood", 60);
        AddItem("Steel", 14);
        AddItem("Wood", 14);
        Debug.Log(inventory[0].Item + " " + inventory[0].Quantity);
        Debug.Log(inventory[1].Item + " " + inventory[1].Quantity);
        Debug.Log(inventory[2].Item + " " + inventory[2].Quantity);
    }

    /// <summary>
    /// To add an item must : 
    /// Check if item not already in inventory, if so, add number to the current stack
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public void AddItem(string item, int number)
    {
        // go through whole inventory
        foreach(var (key, value) in inventory)
        {
            // If picked up item is inside the inventory
            if(value.Item == item)
            {
                int total = value.Quantity + number;
                // Add to the current stack
                value.Quantity = Mathf.Clamp(total, 0, maxItemInStack);
                // If go past the limit of stack 
                if(total > maxItemInStack)
                {
                    // Update number and create a new stack of the item
                    number = total - maxItemInStack;
                    inventory.Add(inventory.Count, new InventoryItem<string, int>(item, number));
                    return;
                }
            }
        }
        // If new entry
        inventory.Add(inventory.Count, new InventoryItem<string, int>(item, number));
    }

    /// <summary>
    /// Class custom std::pair for the inventory
    /// First is the item
    /// Second is the quantity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class InventoryItem<T,U>
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

