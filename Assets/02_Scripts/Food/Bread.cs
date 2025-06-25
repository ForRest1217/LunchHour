using UnityEngine;
using UnityEngine.InputSystem;

public class Bread : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    private bool isstayin = false;


    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isstayin && HandFood.Instance.whathand == 6)
        {
            SoundManager.Instance.PlaySound(soundData);
            GetBread();
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

    private void GetBread()
    {
        HandFood.Instance.ChangeFood(2);
    }
}
