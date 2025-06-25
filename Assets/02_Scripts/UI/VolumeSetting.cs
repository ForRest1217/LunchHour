using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGM();
            SetSFX();
        }
    }

    public void SetBGM()
    {
        float volume = bgmSlider.value;
        audiomixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
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
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SystemVolume");
        SetBGM();
        SetSFX();
    }
}
