using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Desk : MonoBehaviour
{
    [SerializeField] private Sprite nomalsprite;
    [SerializeField] private Sprite colasprite;
    [SerializeField] private Sprite hamburgersprite;
    [SerializeField] private Sprite friedsprite;
    [SerializeField] private Sprite notpattesprite;
    [SerializeField] private Sprite pattesprite;
    [SerializeField] private Sprite saladsprite;

    [SerializeField] private SoundData soundData;

    private SpriteRenderer spriterender;

    private bool isstayin = false;
    private bool cooltime = false;
    private int deskfood = 0;

    private void Awake()
    {
        spriterender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin && HandFood.Instance.whathand > 0 && !cooltime && deskfood == 0)
        {
            if (HandFood.Instance.whathand == 1)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 1;
                StartCoroutine(Cooltime());
            }
            else if (HandFood.Instance.whathand == 2)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 2;
                StartCoroutine(Cooltime());
            }
            else if (HandFood.Instance.whathand == 3)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 3;
                StartCoroutine(Cooltime());
            }
            else if (HandFood.Instance.whathand == 4)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 4;
                StartCoroutine(Cooltime());
            }
            else if (HandFood.Instance.whathand == 5)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 5;
                StartCoroutine(Cooltime());
            }
            else if (HandFood.Instance.whathand == 6)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                deskfood = 6;
                StartCoroutine(Cooltime());
            }
            else if (deskfood > 0)
            {
                SoundManager.Instance.PlaySound(soundData);
                StartCoroutine(HandFood.Instance.HandFool());
                StartCoroutine(Cooltime());
            }
        }
        else if (HandFood.Instance.whathand == 0 && !cooltime && HandFood.Instance != null && Keyboard.current.eKey.wasPressedThisFrame && isstayin && deskfood > 0)
        {
            SoundManager.Instance.PlaySound(soundData);
            HandFood.Instance.ChangeFood(deskfood);
            deskfood = 0;
            StartCoroutine(Cooltime());
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin && HandFood.Instance.whathand > 0 && !cooltime && deskfood > 0)
        {
            SoundManager.Instance.PlaySound(soundData);
            HandFood.Instance.HandFool();
        }

        SpriteRender();
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

    private void SpriteRender()
    {
        if (deskfood == 0)
            spriterender.sprite = nomalsprite;
        else if (deskfood == 1)
            spriterender.sprite = colasprite;
        else if (deskfood == 2)
            spriterender.sprite = hamburgersprite;
        else if (deskfood == 3)
            spriterender.sprite = friedsprite;
        else if (deskfood == 4)
            spriterender.sprite = saladsprite;
        else if (deskfood == 5)
            spriterender.sprite = notpattesprite;
        else if (deskfood == 6)
            spriterender.sprite = pattesprite;
    }

    private IEnumerator Cooltime()
    {
        cooltime = true;
        yield return new WaitForSeconds(0.5f);
        cooltime = false;
    }
}
