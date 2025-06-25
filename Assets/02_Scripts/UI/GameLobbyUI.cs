using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject[] pannel = new GameObject[4];
    [SerializeField] private SoundData soundData;

    private int pannelcount = 0;
    private bool clicked = false;

    private void Awake()
    {
        pannelcount = 0;
        pannel[0].SetActive(false);
        pannel[1].SetActive(false);
        pannel[2].SetActive(false);
        pannel[3].SetActive(false);
        gameObject.SetActive(true);
    }

    public void GameStart()
    {
        if (!clicked)
        {
            StartCoroutine(StartButtonClick());
        }
    }

    public void TutoStart()
    {
        if (!clicked)
        {
            StartCoroutine(TutoButtonClick());
        }
    }

    public void StoryStart()
    {
        if (!clicked)
        {
            StartCoroutine(StoryButtonClick());
        }
    }

    public void InfiStart()
    {
        if (!clicked)
        {
            StartCoroutine(InfiButtonClick());
        }
    }

    public void Option()
    {
        if (!clicked)
        {
            StartCoroutine(OptionButtonClick());
        }
    }

    public void Quit()
    {
        if (!clicked)
        {
            Application.Quit();
        }
    }

    public void Back()
    {
        if (!clicked)
        {
            StartCoroutine(BackButtonClick());
        }
    }

    public void BackMode()
    {
        if (!clicked)
        {
            StartCoroutine(BackModeButtonClick());
        }
    }

    private IEnumerator BackModeButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Mode");
    }

    private IEnumerator TutoButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Tuto");
    }

    private IEnumerator StoryButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Nomal");
    }

    private IEnumerator InfiButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Hard");
    }

    private IEnumerator StartButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Mode");
    }

    private IEnumerator OptionButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Option");
    }

    private IEnumerator BackButtonClick()
    {
        SoundManager.Instance.PlaySound(soundData);
        yield return StartCoroutine(Wait());
        StartCoroutine(SetActivePannel());
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator SetActivePannel()
    {
        for (; pannelcount < pannel.Length; pannelcount++)
        {
            pannel[pannelcount].SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
    }
}
