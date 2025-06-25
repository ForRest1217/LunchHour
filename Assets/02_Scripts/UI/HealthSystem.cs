using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject health;
    [SerializeField] private SoundData soundData;
    [SerializeField] private RectTransform gameoverUI;
    [SerializeField] private GameObject wavet;
    [SerializeField] private GameObject scoret;
    [SerializeField] private GameObject wavetextobject;
    [SerializeField] private TMP_Text wavetext;
    [SerializeField] private GameObject scoretextobject;
    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private SoundData soundDatapop;
    [SerializeField] private GameObject backbutton;
    private GameObject[] hearts;


    public int healthCount { get; set; } = 3;
    public bool isend { get; set; } = false;
    public bool sounddeath = false;
    public bool uiget = false;

    public static HealthSystem Instance = null;
    private void Start()
    {
        wavet.SetActive(false);
        scoret.SetActive(false);
        wavetextobject.SetActive(false);
        scoretextobject.SetActive(false);
        backbutton.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        hearts = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            hearts[i] = Instantiate(health, transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < healthCount)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }

        if (healthCount == 0)
        {
            Time.timeScale = 0;
            isend = true;
            if (!sounddeath)
            {
                //SoundManager.Instance.PlaySound(soundData);
                sounddeath = true;
            }
            gameoverUI.DOAnchorPos(new Vector2(-30, 180), 2f).SetEase(Ease.OutCubic).SetUpdate(true);
            if (!uiget)
                StartCoroutine(Finish());
        }
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSecondsRealtime(1);
        uiget = true;
        SoundManager.Instance.PlaySound(soundDatapop);
        wavet.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        SoundManager.Instance.PlaySound(soundDatapop);
        wavetext.text = Wave.Instance.wave.ToString();
        wavetextobject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        SoundManager.Instance.PlaySound(soundDatapop);
        scoret.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        SoundManager.Instance.PlaySound(soundDatapop);
        scoretext.text = Wave.Instance.score.ToString();
        scoretextobject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        SoundManager.Instance.PlaySound(soundDatapop);
        backbutton.SetActive(true);
    }
}

