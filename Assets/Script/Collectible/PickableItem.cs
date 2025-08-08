using UnityEngine;

public class PickableItem : Collectible
{
    [SerializeField] Item_Main_SO item;
    [SerializeField] int quantity;
    public override void PickUpAction(GameObject target)
    {
        target.GetComponent<InventorySystem>().AddItem(item, quantity);
        Debug.Log("this player got : " + item.itemName + " in this quantity : " + quantity);
        Destroy(gameObject);
    }
}
