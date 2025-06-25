using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FriedMachine : MonoBehaviour
{
    [SerializeField] private GameObject sliderObjectfried;
    [SerializeField] private Slider sliderfried;

    private SpriteRenderer spriterender;
    [SerializeField] private Sprite nomalsprite;
    [SerializeField] private Sprite cooksprite;

    [SerializeField] private SoundData soundData;


    private bool isstayin = false;
    private bool fryfinish = false;
    private bool frying = false;
    private bool cooltime = false;

    private float nowtime = 0;
    private float maxtime;

    private void Awake()
    {
        spriterender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriterender.sprite = nomalsprite;
        sliderObjectfried.SetActive(false);
        maxtime = 5;
        sliderfried.maxValue = maxtime;
    }

    private void Update()
    {
        if (sliderObjectfried.activeSelf)
        {
            nowtime += Time.deltaTime;
            sliderfried.value = nowtime;
            spriterender.sprite = cooksprite;
        }

        if (nowtime > maxtime)
        {
            sliderObjectfried.SetActive(false);
            fryfinish = true;
            if (Keyboard.current.eKey.wasPressedThisFrame && isstayin)
            {
                if (HandFood.Instance.whathand < 1)
                {
                    HandFood.Instance.ChangeFood(3);
                    frying = false;
                    fryfinish = false;
                    nowtime = 0;
                    spriterender.sprite = nomalsprite;
                    StartCoroutine(Cooltime());
                }
                else
                {
                    StartCoroutine(HandFood.Instance.HandFool());
                }
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin)
        {
            if (!fryfinish && !frying && !cooltime)
            {
                SoundManager.Instance.PlaySound(soundData);
                FriedStart();
            }
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

    private void FriedStart()
    {
        frying = true;
        nowtime = 0;
        sliderObjectfried.SetActive(true);
    }

    private IEnumerator Cooltime()
    {
        cooltime = true;
        yield return new WaitForSeconds(0.1f);
        cooltime = false;
    }
}
