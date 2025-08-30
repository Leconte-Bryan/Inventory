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
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isBeingDragged = false;
    [SerializeField] Image slotIcone;
    private void Start()
    {
        ClearSlot();
        canvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Update slot data and UI
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_quantity"></param>
    public void UpdateSlot(Item_Main_SO _item, int _quantity)
    {
        slotImg.color = new Color(1, 1, 1, 1);
        item = _item;
        Debug.Log("sprite is : " + _item.sprite);
        slotImg.sprite = _item.sprite;
        quantityTxt.text = _quantity.ToString();
        quantity = _quantity;
        if (quantity <= 0)
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
        slotImg.color = new Color(1, 1, 1, 0);
    }

    public void ResetPostDrag()
    {
        slotImg.transform.SetParent(transform);
        slotImg.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
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
        if (_item == item)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Send data from a item to the other
    /// </summary>
    /// <param name="craftedItem"></param>
    /// <param name="number"></param>
    /// <param name="destination"></param>
    public void SwapItem(Slot origin, Slot destination)
    {
        Debug.Log("swapping item");
        Item_Main_SO _item = destination.item;
        int _quantity = destination.quantity;
        UpdateSlot(origin.item, origin.quantity);
        origin.UpdateSlot(_item, _quantity);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("user enter the slot");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isBeingDragged)
        {

            Debug.Log("user dragging the slot" + eventData.delta);
            //slotImg.rectTransform.anchoredPosition += eventData.delta * 5;

            slotImg.transform.position = Input.mousePosition;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // If not item, don t drag
        if (!item)
        {
            return;
        }
        //Debug.Log("user start dragging the slot");
        isBeingDragged = true;
        slotImg.transform.SetParent(canvas.transform);
    }





    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item drop in this slot first : " + gameObject.name);
        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;
        Debug.Log("dragged object is : " + draggedObject);

        // Si object drag existe, si le slot n'est pas le même
        if (draggedObject != null && draggedObject.name != gameObject.name && draggedObject.GetComponent<Slot>().isBeingDragged)
        {
            Debug.Log("Item drop in this slot second : " + gameObject.name);
            Slot slotData = draggedObject.GetComponent<Slot>();
            // Snap the dragged object to this slot
            if (!IsEmpty())
            {
                if (CompareItem(slotData.item))
                {
                    if (IsFull())
                    {
                        SwapItem(slotData, this);
                    }
                    else
                    {
                        int total = quantity + slotData.quantity;
                        quantity = Mathf.Clamp(total, 0, item.maxItemInStack);
                        int rest = total - quantity;
                        UpdateSlot(slotData.item, quantity);
                        slotData.UpdateSlot(slotData.item, rest);
                    }
                }
                else
                {
                    SwapItem(slotData, this);
                }
                draggedObject.GetComponent<Slot>().isBeingDragged = false;
                return;
            }
            if (IsEmpty())
            {

                UpdateSlot(slotData.item, slotData.quantity);

                slotData.ClearSlot();
            }
            draggedObject.GetComponent<Slot>().isBeingDragged = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("user released the slot");
        //Debug.Log(slotImg.rectTransform);


        GameObject draggedObject = eventData.pointerDrag;


        if (item && draggedObject != null && isBeingDragged)
        {
            // TODO : different script for drag behavior only
            // Different kind of drop : one by one; quantity / 2 (CeilToInt); all;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //Debug.Log(hit.point);
                quantity--;
                UpdateSlot(item, quantity);
                GameEvents.OnObjectThrow?.Invoke(item);
            }
        }
        ResetPostDrag();

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
