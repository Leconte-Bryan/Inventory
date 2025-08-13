using UnityEngine;

public enum ItemType { HEAL, DAMAGE, PICKABLE }
public class Interactable : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField] public bool affectOnlyPlayer;
    [SerializeField] AudioClip interactionSound;
    [SerializeField] float rotatingSpeed = 50f;

    Vector3 originalPos;

    [Header("Sinusoide")]
    [SerializeField] float yValue; // Updated value
    [SerializeField] float xValue; // Updated value
    [SerializeField] float sinusoideSpeed = 3; // Speed at wich the sinusoide goes (the frequency)
    [SerializeField] float amplitude = 0.5f; // augment maximum and minimum


    private void Start()
    {
        originalPos = transform.position; // Stock the original position of the object
    }

    private void Update()
    {
        // current value of the sinusoide (will oscile between amplitude & -amplitude)
        yValue = originalPos.y + Mathf.Sin(Time.time * sinusoideSpeed) * amplitude;
        // current value of the sinusoide (will oscile between amplitude & -amplitude)
        // xValue = originalPos.x + Mathf.Cos(Time.time * sinusoideSpeed) * amplitude;
        //float sinusoideValueY = Mathf.Clamp(originalPos.y + yValue, originalPos.y - amplitude, originalPos.y + amplitude);
        gameObject.transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
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
