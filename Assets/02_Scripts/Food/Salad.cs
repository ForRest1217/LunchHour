using UnityEngine;
using UnityEngine.InputSystem;

public class Salad : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    private bool isstayin = false;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin)
        {
            if (HandFood.Instance.whathand < 1)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(4);
            }
            else
            {
                SoundManager.Instance.PlaySound(soundData);
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
}
