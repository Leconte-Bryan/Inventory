using UnityEngine;

public class PickableItem : Interactable
{
    public Item_Main_SO item;
    public int quantity;
    public bool canStack = true;

    /// <summary>
    /// From the item throwed to the item on the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        PickableItem otherItem = other.GetComponent<PickableItem>();

        // Check if it s a pickable, same item, can stack (security, prevent to add quantity to multiple objects)
        if (otherItem && otherItem.item == item && otherItem.canBeInteractedWith && canStack)
        {
            canStack = false;
            // Check if same item
            if (otherItem && otherItem.item == item)
            {
                otherItem.quantity += quantity;
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Item_Main_SO _item, int _quantity, bool _affectOnlyPlayer, ItemType _itemType)
    {
        item = _item;
        quantity = _quantity;
        affectOnlyPlayer = _affectOnlyPlayer;
        itemType = _itemType;
        canBeInteractedWith = false;
    }

    public override void PickUpAction(GameObject target)
    {
        Debug.Log("this player got : " + item.itemName + " in this quantity : " + quantity);
    }

    public void GetTheRest(int rest)
    {
        quantity = rest;
        if (CheckIfEmpty())
        {
            Destroy(gameObject);
        }
    }

    bool CheckIfEmpty()
    {
        return quantity == 0 ? true : false;
    }

}
