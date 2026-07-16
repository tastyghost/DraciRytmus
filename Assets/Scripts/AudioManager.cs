using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private const float DefaultEffectsVolume = 0.8f;
    private const float VoiceVolumeScale = 8f;

    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip popClip;
    [SerializeField] private AudioClip antiPopClip;
    [SerializeField] private AudioClip successClip;

    private AudioSource audioSource;

    public float PopClipLength
    {
        get { return popClip != null ? popClip.length : 0f; }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureInstanceExists()
    {
        if (Instance != null || FindFirstObjectByType<AudioManager>() != null)
        {
            return;
        }

        GameObject audioManagerObject = new GameObject("AudioManager");
        audioManagerObject.AddComponent<AudioManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = DefaultEffectsVolume;

        LoadMissingClips();
    }

    public void PlayPop()
    {
        PlayEffect(popClip);
    }

    public void PlayAntiPop()
    {
        PlayEffect(antiPopClip);
    }

    public void PlaySuccess()
    {
        PlayEffect(successClip);
    }

    public void PlayWordCardClip(AudioClip clip)
    {
        PlayEffect(clip, VoiceVolumeScale);
    }

    public void PlayLocationIntroClip(AudioClip clip)
    {
        StopAudio();
        PlayEffect(clip, VoiceVolumeScale);
    }

    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void LoadMissingClips()
    {
        if (popClip == null)
        {
            popClip = Resources.Load<AudioClip>("Audio/Pop");
        }

        if (antiPopClip == null)
        {
            antiPopClip = Resources.Load<AudioClip>("Audio/AntiPop");
        }

        if (successClip == null)
        {
            successClip = Resources.Load<AudioClip>("Audio/Success");
        }
    }

    private void PlayEffect(AudioClip clip, float volumeScale = 1f)
    {
        if (audioSource == null || clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip, volumeScale);
    }
}
