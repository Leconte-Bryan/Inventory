using UnityEngine;

public class Interactor : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("something collide : " + other.name);
        Interactable item = other.GetComponent<Interactable>();
        if (item && item.CanInteract())
        {
            Debug.Log("C'est un collectible ! ");
            if (item.affectOnlyPlayer && tag != "Player")
            {
                Debug.Log("Tu n'est pas un joueur l'objet n'est pas pour toi ! ");
                return;
            }
            if (item.itemType == ItemType.PICKABLE)
            {
                PickableItem pickable = item.GetComponent<PickableItem>();
                gameObject.GetComponent<InventorySystem>().AddItem(pickable, pickable.quantity);
            }
            else
            {
                other.GetComponent<Interactable>().PlayInteractionSound();
                other.GetComponent<Interactable>().PickUpAction(gameObject);
            }
        }
    }
}
