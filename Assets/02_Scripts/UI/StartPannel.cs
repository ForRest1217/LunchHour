using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPannel : MonoBehaviour
{
    [SerializeField] private RectTransform pannel;
    [SerializeField] private SoundData soundData;

    private void Start()
    {
        StartCoroutine(Cooltime());
    }

    private IEnumerator Cooltime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        bool done = false;
        SoundManager.Instance.PlaySound(soundData);
        pannel.DOAnchorPos(new Vector2(0, 1000), 2f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(() => done = true);
        Time.timeScale = 1;
        yield return new WaitUntil(() => done);
    }

    public void StartPannelDown()
    {
        StartCoroutine(Cooltimeaa());
    }

    private IEnumerator Cooltimeaa()
    {
        Time.timeScale = 0;
        SoundManager.Instance.PlaySound(soundData);
        pannel.DOAnchorPos(new Vector2(0, 0), 2f).SetEase(Ease.OutCubic).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        SceneManager.LoadScene("Mode");
    }
}
