using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private Slider musicSlider;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetVolume(musicSlider.value); // Set volume first

        if (backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }

        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
    }


    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        Debug.Log("Playing background music: " + audioClip?.name);

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else if (audioSource.clip != null)
        {
            if (resetSong)
            {
                audioSource.Stop();
            }
        }

        audioSource.Play();
    }


    public void StopBackgroundMusic()
    {
        audioSource.Pause();
    }
}
    