using UnityEngine;

public enum ItemType { HEAL, DAMAGE, PICKABLE }
public class Interactable : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField] public bool affectOnlyPlayer;
    [SerializeField] AudioClip interactionSound;
    [SerializeField] float rotatingSpeed = 50f;

    [Header("Sinusoide")]
    Vector3 originalPos;
    [SerializeField] float yValue; // Updated value
    [SerializeField] float xValue; // Updated value
    [SerializeField] float sinusoideSpeed = 3; // Speed at wich the sinusoide goes (the frequency)
    [SerializeField] float amplitude = 0.5f; // augment maximum and minimum


    private void Start()
    {
        originalPos = transform.position; // Stock the original position of the object
        float rand = Random.Range(0f, 1f);
        /*if (rand <= 0.5)
        {
            rotatingSpeed = -rotatingSpeed;
        }
        */
    }

    private void Update()
    {
        gameObject.transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
        // current value of the sinusoide (will oscile between amplitude & -amplitude)
        yValue = originalPos.y + Mathf.Sin(Time.time * sinusoideSpeed) * amplitude;
        // Apply sinusoide to the y axis (add the sinusoide value to the originalPos.y)
        gameObject.transform.position = new Vector3(originalPos.x, yValue, originalPos.z);
    }

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
