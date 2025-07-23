using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;

    public void Start()
    {
        mixer.GetFloat("VOLUME", out value);
        volumeSlider.value = value;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop Play Mode
#else
        Application.Quit(); // Quit game in build
#endif
    }

    public void SetVolume()
    {
        mixer.SetFloat("VOLUME", volumeSlider.value);
    }
}
