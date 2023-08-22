using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerSnapshot pause;
    public AudioMixerSnapshot unpause;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    public void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(settingsMenu.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(settingsMenu.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void PauseMenuMusic()
    {
        pause.TransitionTo(.01f*Time.timeScale);
    }

    public void UnpauseMusic()
    {
        unpause.TransitionTo(.01f*Time.timeScale);
    }
}
