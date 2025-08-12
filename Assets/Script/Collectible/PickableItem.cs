using UnityEngine;

public class PickableItem : Interactable
{
    public Item_Main_SO item;
    public int quantity;

    public void Initialize(Item_Main_SO _item, int _quantity, bool _affectOnlyPlayer, ItemType _itemType)
    {
        item = _item;
        quantity = _quantity;
        affectOnlyPlayer = _affectOnlyPlayer;
        itemType = _itemType;
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
