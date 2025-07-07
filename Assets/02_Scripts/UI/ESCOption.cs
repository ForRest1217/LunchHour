using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ESCOption : MonoBehaviour
{
    [SerializeField] private RectTransform pannel;
    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private RectTransform sfxSlider;
    [SerializeField] private RectTransform back;
    [SerializeField] private RectTransform lobby;
    [SerializeField] private StartPannel startpannel;

    [SerializeField] private SoundData soundData;

    private bool up = false;
    private bool button = false;
    public bool esc { get; private set; } = false;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !up && !button)
        {
            MouseButton();
        }
    }

    public void BackButton()
    {
        esc = false;
        SoundManager.Instance.PlaySound(soundData);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        float bgm = PlayerPrefs.GetFloat("BGMVolume");
        audiomixer.SetFloat("BGMVolume", bgm);
        float sfx = PlayerPrefs.GetFloat("SFXVolume");
        audiomixer.SetFloat("SFXVolume", sfx);
        up = false;
        button = false;
        esc = false;
        pannel.DOAnchorPos(new Vector2(0, -1121), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
        sfxSlider.DOAnchorPos(new Vector2(0, -1223), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
        back.DOAnchorPos(new Vector2(-369, -1399), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
        lobby.DOAnchorPos(new Vector2(13, -1399), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
    }

    public void MouseButton()
    {
        if (!button)
        {
            esc = true;
            button = true;
            SoundManager.Instance.PlaySound(soundData);
            Time.timeScale = 0f;
            AudioListener.pause = true;
            audiomixer.SetFloat("BGMVolume", -80f);
            audiomixer.SetFloat("SFXVolume", -80f);
            up = true;
            pannel.DOAnchorPos(new Vector2(0, 0), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
            sfxSlider.DOAnchorPos(new Vector2(0, 0), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
            back.DOAnchorPos(new Vector2(-369, -277), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
            lobby.DOAnchorPos(new Vector2(13, -277), 0.7f).SetEase(Ease.OutCubic).SetUpdate(true);
        }
    }

    public void LobbyButton()
    {
        esc = false;
        Time.timeScale = 1f;
        SoundManager.Instance.PlaySound(soundData);
        AudioListener.pause = false;
        up = false;
        button = false;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        startpannel.StartPannelDown();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Lobby");
    }
}

