using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public int id;
    public Image slotImg;
    public TMP_Text quantityTxt;
    public Item_Main_SO item;
    public int quantity;
    private Canvas canvas;
    private CanvasGroup CanvasGroup;
    private bool isBeingDragged = false;
    private void Start()
    {
        ClearSlot();
        canvas = GetComponentInParent<Canvas>();
        CanvasGroup = slotImg.GetComponent<CanvasGroup>();
    }

/// <summary>
/// Update slot data and UI
/// </summary>
/// <param name="_item"></param>
/// <param name="_quantity"></param>
public void UpdateSlot(Item_Main_SO _item, int _quantity)
    {
        item = _item;
        slotImg.sprite = _item.sprite;
        quantityTxt.text = _quantity.ToString();
        quantity = _quantity;
        if(quantity <= 0)
        {
            ClearSlot();
        }
    }

    /// <summary>
    /// When item is clicked, something happen
    /// </summary>
    public void UseItem(int _quantity)
    {
        if (item && !isBeingDragged)
        {
            quantity -= Mathf.Clamp(_quantity, 0, item.maxItemInStack);
            quantityTxt.text = quantity.ToString();
            if (quantity <= 0)
            {
                ClearSlot();
            }
        }
    }

    /// <summary>
    /// Clear data and UI
    /// </summary>
    public void ClearSlot()
    {
        item = null;
        slotImg.sprite = null;
        quantityTxt.text = "";
        quantity = 0;
    }

    public void ResetPostDrag() {
        slotImg.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        CanvasGroup.alpha = 1;
        isBeingDragged = false;
    }

    /// <summary>
    /// Check if item inside slot
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        if (item)
        {
            return false;
        }
        return true;
    }

    public bool IsFull()
    {
        if (item && quantity == item.maxItemInStack)
        {
            return true;
        }
        return false;
    }

    public bool CompareItem(Item_Main_SO _item)
    {
        if(_item == item)
        {
            return true;
        }
        return false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("user enter the slot");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isBeingDragged)
        {
            Debug.Log("user dragging the slot");
            slotImg.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("user start dragging the slot");
        CanvasGroup.alpha = 0.6f;
        isBeingDragged = true;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("user released the slot");
        Debug.Log(slotImg.rectTransform);


        GameObject draggedObject = eventData.pointerDrag;


        if (draggedObject != null && isBeingDragged)
        {

            // TODO : different script for drag behavior only
            // Different kind of drop : one by one; quantity / 2 (CeilToInt); all;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Debug.Log(hit.point);
                PickableItem ItemDropped = Instantiate(item.DropModel, hit.point, Quaternion.identity);
                ItemDropped.Initialize(item, 1, item.DropModel.affectOnlyPlayer, item.DropModel.itemType);
                quantity--;
                UpdateSlot(item, quantity);
            }
        }
        ResetPostDrag();

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped on Slot");

        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null && isBeingDragged)
        {
            Slot slotData = draggedObject.GetComponent<Slot>();
            // Snap the dragged object to this slot
            if (!IsFull() && CompareItem(slotData.item)){
                int total = quantity + slotData.quantity;
                quantity = Mathf.Clamp(total, 0, item.maxItemInStack);
                int rest = total - quantity;
                UpdateSlot(slotData.item, quantity);
                slotData.UpdateSlot(slotData.item, rest);
                return;

            }
            if (IsEmpty())
            {
                UpdateSlot(slotData.item, slotData.quantity);
                slotData.ClearSlot();
            }
        }
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);

        Gizmos.DrawCube(slotImg.transform.position, new Vector3(1, 1, 1));
        Debug.Log("drawing gizmos");
    }
    */
}
