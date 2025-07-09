using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = sfxVolume;
        audiomixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20f);
        audiomixer.SetFloat("SystemVolume", Mathf.Log10(sfxVolume) * 20f);

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFX();
        }
    }

    public void SetSFX()
    {
        float volume = sfxSlider.value;
        audiomixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audiomixer.SetFloat("SystemVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SystemVolume", volume);
    }

    private void LoadVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SystemVolume");
        SetSFX();
    }
}
