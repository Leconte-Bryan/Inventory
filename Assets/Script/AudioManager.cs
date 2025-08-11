using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;


    GameEvents OnPlaySFX;
    static public AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("exist");

        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameEvents.OnPlaySFX += PlayAudioClip;
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        Debug.Log("a sound should play");
        audioSource.PlayOneShot(audioClip);
    }
}
