using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private ESCOption escoption;

    private void OnEnable()
    {
        GameObject escObj = GameObject.Find("ESCOption");
        if (escObj != null)
            escoption = escObj.GetComponent<ESCOption>();
    }

    public async void PlaySound(SoundData soundData)
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = soundData.audioClip;

        if (soundData.soundType == SoundType.SFX && escoption != null && escoption.esc == true)
        {
            gameObject.SetActive(false);
        }
        else if (soundData.soundType == SoundType.System && escoption != null && escoption.esc == true)
        {
            _audioSource.ignoreListenerPause = true;
            _audioSource.Play();
        }
        else
        {
            _audioSource.Play();
        }

        await Awaitable.WaitForSecondsAsync(10);
        if (soundData.soundType != SoundType.BGM)
            Destroy(gameObject);
    }
}
