using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    public static AudioManager instance;
    public float musicVolume, sfxVolume, masterVolume;
    public Sound[] sounds;

    public bool musicMute;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        //make the playerprefs keys for the first time
        if (PlayerPrefs.HasKey("masterVol") == false)
        {
            PlayerPrefs.SetFloat("masterVol", 1);
            print("master key not found");
        }
        if (PlayerPrefs.HasKey("musicVol") == false)
        {
            PlayerPrefs.SetFloat("musicVol", 1);
            print("music key not found");
        }
        if (PlayerPrefs.HasKey("sfxVol") == false)
        {
            PlayerPrefs.SetFloat("sfxVol", 1);
            print("sfx key not found");
        }

        musicMute = false;

        if (PlayerPrefs.HasKey("mute") == false)
        {
            PlayerPrefs.SetInt("mute", 0);
        }

        masterVolume = PlayerPrefs.GetFloat("masterVol");
        musicVolume = PlayerPrefs.GetFloat("musicVol");
        sfxVolume = PlayerPrefs.GetFloat("sfxVol");


        print("master=" + masterVolume);
        print("music=" + musicVolume);
        print("sfx=" + sfxVolume);
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("masterVol", masterVolume);
        PlayerPrefs.SetFloat("musicVol", musicVolume);
        PlayerPrefs.SetFloat("sfxVol", sfxVolume);
    }

    //play music clip
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            print("Sound: " + name + "  not found");
            return;
        }

        print("playing music " + name);
        s.source.Play();
    }

    //play sfx clip
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            print("Sound: " + name + "  not found");
            return;
        }

        print("playing sfx " + name);
        s.source.volume = sfxVolume;
        s.source.Play();
    }

    public void ChangeAudioSourceVolume(string name, float vol)
    {
        Sound s = Array.Find(sounds, AudioSystem => AudioSystem.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found!");
            return;
        }
        s.source.volume = vol;
    }
}