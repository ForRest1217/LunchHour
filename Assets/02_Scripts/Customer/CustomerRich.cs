using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerRich : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject[] food;
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

    private SpriteRenderer spriterenderer;
    [SerializeField] private Sprite two;
    [SerializeField] private Sprite one;

    List<int> emptySeats = new List<int>();

    private int randomspawnint = 0;
    private int wantfood = 0;
    private int speed = 2;

    private bool arrived = false;
    private bool randomwantfoodget = false;
    private bool randomfoodget = false;
    private bool randomfoodtwoget = false;
    private bool randomfoodthreeget = false;
    private bool isstayin = false;
    private bool isReturning = false;

    private float nowtime = 0;

    private void Awake()
    {
        spriterenderer = GameObject.Find("menu").GetComponentInChildren<SpriteRenderer>();
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
            spawnslider.maxValue = 40f;
        else if (SceneManager.GetActiveScene().name == "Hard")
            spawnslider.maxValue = 35f;
        spawnslider.value = nowtime;
        handleImage = spawnslider.handleRect.GetComponent<Image>();
        handleImage.sprite = handlehappy;

        ani.SetBool("walk", true);

        menu.SetActive(false);
        for (int i = 0; i < food.Length; i++)
            food[i].SetActive(false);

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
        else if (Vector2.Distance(transform.position, random) < 0.01f && !arrived)
        {
            spawned.SetActive(true);
            arrived = true;
            ani.SetBool("walk", false);
            menu.SetActive(true);
            if (!randomwantfoodget)
            {
                wantfood = Random.Range(1, 5);
                randomwantfoodget = true;
            }
            food[wantfood - 1].SetActive(true);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && arrived && isstayin)
        {
            if (wantfood == HandFood.Instance.whathand && !randomfoodget && !randomfoodtwoget && !randomfoodthreeget)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                randomfoodget = true;
                spriterenderer.sprite = two;
                nowtime -= 10;
            }
            else if (wantfood == HandFood.Instance.whathand && !randomfoodtwoget && !randomfoodthreeget)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                randomfoodtwoget = true;
                spriterenderer.sprite = one;
                nowtime -= 10;
            }
            else if (wantfood == HandFood.Instance.whathand && !randomfoodthreeget)
            {
                SoundManager.Instance.PlaySound(soundData);
                HandFood.Instance.ChangeFood(0);
                randomfoodthreeget = true;
            }
        }

        if (randomfoodget && randomfoodtwoget && randomfoodthreeget)
        {
            if (isReturning != true)
            {
                ani.SetBool("walkback", true);
                Destroy(spawned);
                spawnslider = null;
                menu.SetActive(false);
                Wave.Instance.score += 1000;
            }
            isReturning = true;
            food[wantfood - 1].SetActive(false);
            transform.position = Vector2.MoveTowards(transform.position, door[randomspawnint], speed * Time.deltaTime);
        }

        if (arrived && Vector2.Distance(transform.position, door[randomspawnint]) < 0.01f)
        {
            Wave.Instance.zari[randomspawnint] = 0;
            Wave.Instance.Customerspawn -= 1;
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
                    if (nowtime < 20)
                        handleImage.sprite = handlehappy;
                    else if (nowtime >= 20)
                        handleImage.sprite = handleangry;
                }
                if (nowtime >= 40)
                {
                    HealthSystem.Instance.healthCount -= 1;
                    randomfoodget = true;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Hard")
            {
                if (handleImage != null)
                {
                    if (nowtime < 17)
                        handleImage.sprite = handlehappy;
                    else if (nowtime >= 17)
                        handleImage.sprite = handleangry;
                }
                if (nowtime >= 35)
                {
                    HealthSystem.Instance.healthCount -= 1;
                    randomfoodget = true;
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
