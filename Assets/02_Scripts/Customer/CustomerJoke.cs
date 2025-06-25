using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerJoke : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private SoundData soundData;

    [SerializeField] private GameObject sliderObject;
    private GameObject spawned;
    [SerializeField] private Slider slider;
    private Slider spawnslider;
    [SerializeField] private Transform parentCanvas;
    private Image handleImage;
    [SerializeField] private Sprite handlehappy;
    [SerializeField] private Sprite handleangry;

    private Vector3[] spawn = { new Vector3(-2.5f, 0.5f, 0f), new Vector3(-0.85f, 0.5f, 0f), new Vector3(0.85f, 0.5f, 0f), new Vector3(2.5f, 0.5f, 0f) };
    private Vector3[] door = { new Vector3(-2.5f, 5f, 0f), new Vector3(-0.85f, 5f, 0f), new Vector3(0.85f, 5f, 0f), new Vector3(2.5f, 5f, 0f) };
    private Vector3 random = new Vector3(0, 0, 0);

    private Animator ani;

    List<int> emptySeats = new List<int>();

    private int randomspawnint = 0;
    private int speed = 2;

    private bool arrived = false;
    private bool isstayin = false;
    private bool back = false;
    private bool isReturning = false;
    private bool foodget = false;

    private float nowtime = 0;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        sliderObject.SetActive(false);
        spawned = Instantiate(sliderObject, parentCanvas);
        spawnslider = spawned.GetComponentInChildren<Slider>();
        spawned.SetActive(false);
        spawnslider.interactable = false;
        if (SceneManager.GetActiveScene().name == "Nomal")
            spawnslider.maxValue = 20f;
        else if (SceneManager.GetActiveScene().name == "Hard")
            spawnslider.maxValue = 15f;
        spawnslider.value = nowtime;
        handleImage = spawnslider.handleRect.GetComponent<Image>();
        handleImage.sprite = handlehappy;

        ani.SetBool("walk", true);
        menu.SetActive(false);

        for (int i = 0; i < Wave.Instance.zari.Length; i++)
            if (Wave.Instance.zari[i] == 0)
                emptySeats.Add(i);
        if (emptySeats.Count > 0)
        {
            randomspawnint = emptySeats[Random.Range(0, emptySeats.Count)];
            random = spawn[randomspawnint];
            Wave.Instance.zari[randomspawnint] = 1;
            transform.position = door[randomspawnint];
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, random) > 0.01f && !arrived)
        {
            transform.position = Vector2.MoveTowards(transform.position, random, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, random) < 0.01f)
        {
            spawned.SetActive(true);
            arrived = true;
            ani.SetBool("walk", false);
            menu.SetActive(true);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && arrived && isstayin && 0 < HandFood.Instance.whathand)
        {
            SoundManager.Instance.PlaySound(soundData);
            HandFood.Instance.ChangeFood(0);
            back = true;
            foodget = true;
        }
        if (back)
        {
            if (isReturning != true)
            {
                ani.SetBool("walkback", true);
                Destroy(spawned);
                spawnslider = null;
                menu.SetActive(false);
                if (foodget)
                    Wave.Instance.score += 100;
            }
            isReturning = true;
            transform.position = Vector2.MoveTowards(transform.position, door[randomspawnint], speed * Time.deltaTime);
        }
        if (arrived && Vector2.Distance(transform.position, door[randomspawnint]) < 0.01f)
        {
            Wave.Instance.zari[randomspawnint] = 0;
            Wave.Instance.Customerspawn--;
            Destroy(gameObject);
        }

        if (spawned != null && spawned.activeSelf && spawnslider != null)
        {
            nowtime += Time.deltaTime;
            spawnslider.value = nowtime;
            if (SceneManager.GetActiveScene().name == "Nomal")
            {
                if (handleImage != null)
                {
                    if (nowtime < 10)
                        handleImage.sprite = handlehappy;
                    else if (nowtime >= 10)
                        handleImage.sprite = handleangry;
                }
                if (nowtime >= 20)
                {
                    HealthSystem.Instance.healthCount -= 1;
                    back = true;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Hard")
            {
                if (handleImage != null)
                {
                    if (nowtime < 8)
                        handleImage.sprite = handlehappy;
                    else if (nowtime >= 8)
                        handleImage.sprite = handleangry;
                }
                if (nowtime >= 15)
                {
                    HealthSystem.Instance.healthCount -= 1;
                    back = true;
                }
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
