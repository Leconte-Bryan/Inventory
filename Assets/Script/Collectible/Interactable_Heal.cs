using UnityEngine;

public class Interactable_Heal : Interactable
{
    [SerializeField] int healValue = 5;
    public override void PickUpAction(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(-healValue);
        Debug.Log("this heal the player : ");
        Destroy(gameObject);
    }
}
