using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentHp = maxHp;;
        currentHp = maxHp / 2;
    }

    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
        if(currentHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("the player is dead");
    }
}
