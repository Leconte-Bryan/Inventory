using UnityEngine;

public class Collectible_Trap : Collectible
{
    [SerializeField] int damageValue = 5;
    public override void PickUpAction(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damageValue);
        Debug.Log("this damage the player : ");
        Destroy(gameObject);
    }
}
