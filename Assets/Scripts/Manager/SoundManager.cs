using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private const string strMusic = "musicVol";
    private const string strEffects = "sfxVol";

    public Slider musicVolume;
    public Slider effectVolume;
    public Toggle soundOnOff;

    public AudioMixer masterMixer;


    private void Start()
    {
        MusicVolumeChanged(musicVolume.value);
        EffectsVolumeChanged(effectVolume.value);
        musicVolume.onValueChanged.AddListener(MusicVolumeChanged);
        effectVolume.onValueChanged.AddListener(EffectsVolumeChanged);
        soundOnOff.onValueChanged.AddListener(VolumeOnOff);
    }

    public void MusicVolumeChanged(float volume)
    {
        masterMixer.SetFloat(strMusic, volume * 100 - 80f);
    }

    public void EffectsVolumeChanged(float volume)
    {
        masterMixer.SetFloat(strEffects, volume * 100 - 80f);
    }

    public void VolumeOnOff(bool on)
    {
        if (on)
        {
            MusicVolumeChanged(musicVolume.value);
            EffectsVolumeChanged(effectVolume.value);
        }
        else
        {
            MusicVolumeChanged(0f);
            EffectsVolumeChanged(0f);
        }
    }
}
