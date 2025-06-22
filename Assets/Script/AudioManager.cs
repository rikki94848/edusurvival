using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    public AudioClip mainMenuBGM;
    public AudioClip gameplayBGM;
    public AudioClip gameOverBGM;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Biar tetap hidup antar scene
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainMenuBGM()
    {
        PlayBGM(mainMenuBGM);
    }

    public void PlayGameplayBGM()
    {
        PlayBGM(gameplayBGM);
    }

    public void PlayGameOverBGM()
    {
        PlayBGM(gameOverBGM);
    }

    private void PlayBGM(AudioClip clip)
    {
        if (clip == null || audioSource.clip == clip) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
