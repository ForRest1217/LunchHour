using UnityEngine;
using UnityEngine.InputSystem;

public class TrashCan : MonoBehaviour
{
    private HandFood handfood;
    private bool isstayin = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        handfood = GameObject.Find("Player").GetComponent<HandFood>();
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin)
        {
            if (HandFood.Instance.whathand > 0)
            {
                audioSource.Play();
                Trash();
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


    private void Trash()
    {
        if (handfood.whathand > 0)
        {
            HandFood.Instance.ChangeFood(0);
        }
    }
}
