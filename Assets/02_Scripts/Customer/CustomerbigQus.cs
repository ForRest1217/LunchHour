using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerbigQus : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject[] food;
    [SerializeField] private GameObject[] foodbig;
    [SerializeField] private SoundData soundData;
    [SerializeField] private GameObject qus;
    [SerializeField] private GameObject qusbig;

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
    private bool randomwantfoodget = false;
    private bool randomfoodget = false;
    private bool randomfoodbigget = false;

    private bool isstayin = false;
    private int wantfood = 0;
    private int wantfoodbig = 0;
    private bool isReturning = false;
    private bool foodget = false;

    private float maxtime;
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
            maxtime = 35f;
        else if (SceneManager.GetActiveScene().name == "Hard")
            maxtime = 30f;
        spawnslider.maxValue = maxtime;
        spawnslider.value = nowtime;
        handleImage = spawnslider.handleRect.GetComponent<Image>();
        handleImage.sprite = handlehappy;

        ani.SetBool("walk", true);

        menu.SetActive(false);
        for (int i = 0; i < food.Length; i++)
            food[i].SetActive(false);
        for (int i = 0; i < foodbig.Length; i++)
            foodbig[i].SetActive(false);

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
                wantfoodbig = Random.Range(1, 5);
                randomwantfoodget = true;
            }

            if (nowtime >= maxtime / 2)
            {
                qus.SetActive(true);
                food[wantfood - 1].SetActive(false);
            }
            else if (nowtime <= maxtime / 2)
            {
                qus.SetActive(false);
                food[wantfood - 1].SetActive(true);
            }

            if (nowtime >= maxtime / 2)
            {
                qusbig.SetActive(true);
                foodbig[wantfoodbig - 1].SetActive(false);
            }
            else if (nowtime <= maxtime / 2)
            {
                qusbig.SetActive(false);
                foodbig[wantfoodbig - 1].SetActive(true);
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && arrived && isstayin)
        {
            if (wantfood == HandFood.Instance.whathand && !randomfoodget)
            {
                SoundManager.Instance.PlaySound(soundData);

                food[wantfood - 1].SetActive(false);
                HandFood.Instance.ChangeFood(0);
                randomfoodget = true;
                nowtime -= 10;
            }
            if (wantfoodbig == HandFood.Instance.whathand && !randomfoodbigget)
            {
                SoundManager.Instance.PlaySound(soundData);
                foodbig[wantfoodbig - 1].SetActive(false);
                HandFood.Instance.ChangeFood(0);
                randomfoodbigget = true;
                foodget = true;
                nowtime -= 10;
            }
        }


        if (randomfoodget && randomfoodbigget)
        {
            if (isReturning != true)
            {
                ani.SetBool("walkback", true);
                Destroy(spawned);
                spawnslider = null;
                menu.SetActive(false);
                if (foodget)
                    Wave.Instance.score += 600;
            }
            isReturning = true;
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
            if (handleImage != null)
            {
                if (nowtime < maxtime / 2)
                    handleImage.sprite = handlehappy;
                else if (nowtime >= maxtime / 2)
                    handleImage.sprite = handleangry;
            }
            if (nowtime >= maxtime)
            {
                randomfoodget = true;
                randomfoodbigget = true;
                HealthSystem.Instance.healthCount -= 1;
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
