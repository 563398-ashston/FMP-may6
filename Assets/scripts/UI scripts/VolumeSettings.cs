using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeSettings : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteMusicButton;
    public AudioMixer mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayMusic("background music");

        //read the bool for the mute button
        if (PlayerPrefs.GetInt("mute") == 0)
        {
            AudioManager.instance.musicMute = false;
        }
        else
        {
            AudioManager.instance.musicMute = true;
        }

        masterSlider.value = AudioManager.instance.masterVolume;
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        musicSlider.value = AudioManager.instance.musicVolume;
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        sfxSlider.value = AudioManager.instance.sfxVolume;
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        muteMusicButton.isOn = AudioManager.instance.musicMute;
    }

    // Update is called once per frame
    void Update()
    {
        //check for mute being pressed
        DoMusicMute();
    }
    void SetMasterVolume(float val)
    {
        val = Mathf.Clamp(val, 0.0001f, 1f);
        //print("mastervol=" + val);

        mixer.SetFloat(AudioManager.MIXER_MASTER, Mathf.Log10(val) * 20);
        AudioManager.instance.masterVolume = val;
    }

    void SetMusicVolume(float val)
    {
        val = Mathf.Clamp(val, 0.0001f, 1f);

        mixer.SetFloat(AudioManager.MIXER_MUSIC, Mathf.Log10(val) * 20);
        AudioManager.instance.musicVolume = val;
    }

    void SetSFXVolume(float val)
    {
        val = Mathf.Clamp(val, 0.0001f, 1f);

        mixer.SetFloat(AudioManager.MIXER_SFX, Mathf.Log10(val) * 20);
        AudioManager.instance.sfxVolume = val;
    }

    void DoMusicMute()
    {
        float vol;
        if (AudioManager.instance.musicMute == true)
        {
            vol = 0.0001f;
        }
        else
        {
            vol = musicSlider.value;
        }

        mixer.SetFloat(AudioManager.MIXER_MUSIC, Mathf.Log10(vol) * 20);
        PlayerPrefs.SetInt("mute", (AudioManager.instance.musicMute ? 1 : 0));
    }
}

