using UnityEngine;

public enum ItemType { HEAL, DAMAGE, PICKABLE }
public class Interactable : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField] public bool affectOnlyPlayer;
    [SerializeField] AudioClip interactionSound;


    public virtual void PickUpAction(GameObject target)
    {
        Debug.Log("player entered the item radius and will pick it up");
        Destroy(gameObject);
    }

    public void PlayInteractionSound()
    {
        GameEvents.OnPlaySFX?.Invoke(interactionSound);
    }

}
