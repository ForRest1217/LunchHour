using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ColaMachine : MonoBehaviour
{
    [SerializeField] private GameObject sliderObject;
    [SerializeField] private Slider slider;
    [SerializeField] private SoundData soundData;

    private float maxtime = 5;
    private float nowtime = 0;
    private bool cooltime = true;
    private bool isstayin = false;

    private void Start()
    {
        sliderObject.SetActive(false);
        slider.maxValue = maxtime;
        maxtime = 5;
        slider.value = nowtime;
    }



    private void Update()
    {
        if (sliderObject.activeSelf)
        {
            nowtime += Time.deltaTime;
            slider.value = nowtime;
        }

        if (nowtime >= maxtime)
        {
            cooltime = true;
            nowtime = 0;
            sliderObject.SetActive(false);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && cooltime && isstayin)
        {
            Cooltime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !HandFood.isTriggered)
        {
            HandFood.isTriggered = true;
            isstayin = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !HandFood.isTriggered)
        {
            HandFood.isTriggered = true;
            isstayin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandFood.isTriggered = false;
            isstayin = false;
        }
    }

    private void Cooltime()
    {
        if (HandFood.Instance.whathand < 1)
        {
            SoundManager.Instance.PlaySound(soundData);
            HandFood.Instance.ChangeFood(1);
            nowtime = 0;
            sliderObject.SetActive(true);
            cooltime = false;
        }
        else
        {
            StartCoroutine(HandFood.Instance.HandFool());
        }
    }
}
