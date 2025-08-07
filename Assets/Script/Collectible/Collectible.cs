using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] bool affectOnlyPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (affectOnlyPlayer && other.gameObject.tag == "Player")
        {
            PickUpAction(other.transform.parent.gameObject);
        }
        else if(!affectOnlyPlayer)
        {
            PickUpAction(other.transform.parent.gameObject);
        }
    }

    public virtual void PickUpAction(GameObject target)
    {
        Debug.Log("player entered the item radius and will pick it up");
        Destroy(gameObject);
    }

}
