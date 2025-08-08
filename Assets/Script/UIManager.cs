using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    public GameObject inventoryPanel;
    public List<Slot> inventorySlots;
    /*
    public List<Image> inventorySlotsImg;
    public List<TMP_Text> quantityTxt;
    */
    static public UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("exist");

        }
        else
        {
            Destroy(this);
        }
    }

    public void DisplayInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void AddItemToTheUI(Item_Main_SO item, int quantity, int slot)
    {
        Debug.Log("slot is equal to : " + slot);
        inventorySlots[slot].slotImg.sprite = item.sprite;
        UpdateItemQuantity(quantity, slot);
    }

    public void UpdateItemQuantity(int quantity, int slot)
    {
        inventorySlots[slot].quantityTxt.text = quantity.ToString();
    }
}
