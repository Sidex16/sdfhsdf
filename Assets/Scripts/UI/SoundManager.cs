using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider global;
    [SerializeField] private Slider effects;
    [SerializeField] private Slider music;

    private int counter = 0;

    private void Start()
    {
        global.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        music.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        effects.value = PlayerPrefs.GetFloat("EffectsVolume", 1);
    }
    public void ChangeGlobalVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ChangeEffectsVolume(float volume)
    {
        mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }

    public void ExitToMain()
    {
        if (counter == 0)
        {
            counter++;
            SceneTransition.SwitchScene("_MainScene");
        }
    }
}
