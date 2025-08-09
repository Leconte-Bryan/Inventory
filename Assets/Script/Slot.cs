using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public int id;
    public Image slotImg;
    public TMP_Text quantityTxt;
    public Item_Main_SO item;
    public int quantity;


    private void Start()
    {
        ClearSlot();
    }

    public void UpdateSlot(Item_Main_SO _item, int _quantity) {
        item = _item;
        slotImg.sprite = _item.sprite;
        quantityTxt.text = _quantity.ToString();
        quantity = _quantity;
    }

    public void UseItem()
    {
        if (item)
        {
            quantity--;
            quantityTxt.text = quantity.ToString();
            if(quantity <= 0)
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        item = null;
        slotImg.sprite = null;
        quantityTxt.text = "";
        quantity = 0;
    }

    public bool IsEmpty()
    {
        if (item)
        {
            return false;
        }
        return true;
    }

}
