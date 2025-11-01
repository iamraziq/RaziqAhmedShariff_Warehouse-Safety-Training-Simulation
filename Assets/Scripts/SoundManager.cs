using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Guide Audio Clips")]
    public AudioClip guide_Barcode;
    public AudioClip guide_Collect;
    public AudioClip guide_Gloves;
    public AudioClip guide_Helmet;
    public AudioClip guide_Quiz;
    public AudioClip guide_Remaining;
    public AudioClip guide_Results;
    public AudioClip guide_TapeGun;
    public AudioClip guide_Failed;
    public AudioClip guide_WelcomeWarehouse;
    public AudioClip guide_WelcomeClassroom;
    public AudioClip guide_FinishClassroom;
    public AudioClip guide_CompleteChecklist;
    public AudioClip guide_Success;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[SoundManager] Tried to play a null AudioClip!");
            return;
        }

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
