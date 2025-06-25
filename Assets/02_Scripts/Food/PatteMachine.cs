using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PatteMachine : MonoBehaviour
{
    [SerializeField] private GameObject sliderObject;
    [SerializeField] private Slider slider;

    [SerializeField] private Sprite nomalsprite;
    [SerializeField] private Sprite cooksprite;
    [SerializeField] private Sprite cookedsprite;

    [SerializeField] private SoundData soundData;

    private SpriteRenderer spriterender;

    private bool isstayin = false;
    private bool cooking = false;
    private bool cookfinish = false;

    private float nowtime = 0;
    private float maxtime;

    private void Awake()
    {
        spriterender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriterender.sprite = nomalsprite;
        sliderObject.SetActive(false);
        maxtime = 5;
        slider.maxValue = maxtime;
    }

    private void Update()
    {
        if (sliderObject.activeSelf)
        {
            nowtime += Time.deltaTime;
            slider.value = nowtime;
            spriterender.sprite = cooksprite;
        }

        if (nowtime > maxtime)
        {
            sliderObject.SetActive(false);
            cookfinish = true;
            cooking = false;
            spriterender.sprite = cookedsprite;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin)
        {
            if (HandFood.Instance.whathand < 1 && cookfinish)
            {
                HandFood.Instance.ChangeFood(6);
                nowtime = 0;
                spriterender.sprite = nomalsprite;
                cookfinish = false;
            }
            else if (HandFood.Instance.whathand == 5 && !cooking && !cookfinish)
            {
                SoundManager.Instance.PlaySound(soundData);
                Fry();
            }
            else if (HandFood.Instance.whathand > 0 && cookfinish)
            {
                StartCoroutine(HandFood.Instance.HandFool());
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

    private void Fry()
    {
        HandFood.Instance.whathand = 0;
        sliderObject.SetActive(true);
        cooking = true;
    }
}
