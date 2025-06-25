using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    [SerializeField] private GameObject[] customerPrefab;
    private Vector3 spawnPoint = new Vector3(0, 5, 0);

    [SerializeField] private RectTransform menuGameObject;
    [SerializeField] private TMP_Text[] menu;
    [SerializeField] private RectTransform hpGameObject;

    [SerializeField] private SoundData soundData;

    public static Wave Instance = null;
    [field: SerializeField] public int[] zari { get; set; } = new int[] { 0, 0, 0, 0 };
    public int Customerspawn { get; set; } = 0;
    public int score { get; set; } = 0;
    private bool ing = false;

    public int wave { get; set; } = 0;
    private int randomcustomer = 0;

    private int targetCount = 0;
    private int spawnedCount = 0;


    private void Start()
    {
        SoundManager.Instance.PlaySound(soundData);
        menuGameObject.DOAnchorPos(new Vector2(-352, 337), 1.5f).SetEase(Ease.OutCubic);
        hpGameObject.DOAnchorPos(new Vector2(15, -210), 1.5f).SetEase(Ease.OutCubic);
        if (Customerspawn == 0 && !ing)
        {
            ing = true;
            StartCoroutine(WaveUp());
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Customerspawn == 0 && !ing)
        {
            ing = true;
            StartCoroutine(WaveUp());
        }
        menu[0].text = $"{wave}";
        menu[1].text = $"{score}";
        menu[2].text = $"{Customerspawn + (targetCount - spawnedCount)}";
    }

    private IEnumerator WaveUp()
    {
        wave++;
        targetCount = 0;
        if (SceneManager.GetActiveScene().name == "Nomal")
        {
            if (wave < 5)
                targetCount = wave;
            else if (wave % 7 == 0)
                targetCount = Random.Range(1, 4);
            else if (wave % 7 != 0 && wave % 10 == 0)
                targetCount = Random.Range(5, 11);
            else if (wave % 7 != 0 && wave % 10 != 0 && wave % 15 == 0)
                targetCount = Random.Range(3, 7);
            else if (wave < 999)
                targetCount = Random.Range(4, 11);

            spawnedCount = 0;
            while (spawnedCount < targetCount)
            {
                if (zari.Contains(0) && Customerspawn < 4)
                {
                    if (wave < 5)
                        yield return StartCoroutine(SpawnWaveChoban());
                    else if (wave % 7 == 0)
                        yield return StartCoroutine(SpawnWaveRich());
                    else if (wave % 7 != 0 && wave % 10 == 0)
                        yield return StartCoroutine(SpawnWaveJoke());
                    else if (wave % 7 != 0 && wave % 10 != 0 && wave % 15 == 0)
                        yield return StartCoroutine(SpawnWaveDont());
                    else if (wave < 10)
                        yield return StartCoroutine(SpawnWaveJungban());
                    else if (wave < 15)
                        yield return StartCoroutine(SpawnWaveHuban());
                    else if (wave < 20)
                        yield return StartCoroutine(SpawnWaveHuJungban());
                    else if (wave < 999)
                        yield return StartCoroutine(SpawnWaveHuHuban());
                    spawnedCount++;
                }
                else
                    yield return null;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Hard")
        {
            if (wave < 5)
                targetCount = wave;
            else if (wave % 7 == 0)
                targetCount = Random.Range(1, 4);
            else if (wave % 7 != 0 && wave % 10 == 0)
                targetCount = Random.Range(5, 11);
            else if (wave % 7 != 0 && wave % 10 != 0 && wave % 15 == 0)
                targetCount = Random.Range(3, 7);
            else if (wave < 999)
                targetCount = Random.Range(4, 11);

            spawnedCount = 0;
            while (spawnedCount < targetCount)
            {
                if (zari.Contains(0) && Customerspawn < 4)
                {
                    if (wave < 5)
                        yield return StartCoroutine(SpawnWaveChobanHard());
                    else if (wave % 7 == 0)
                        yield return StartCoroutine(SpawnWaveRichHard());
                    else if (wave % 7 != 0 && wave % 10 == 0)
                        yield return StartCoroutine(SpawnWaveJokeHard());
                    else if (wave % 7 != 0 && wave % 10 != 0 && wave % 15 == 0)
                        yield return StartCoroutine(SpawnWaveDontHard());
                    else if (wave < 10)
                        yield return StartCoroutine(SpawnWaveJungbanHard());
                    else if (wave < 15)
                        yield return StartCoroutine(SpawnWaveHubanHard());
                    else if (wave < 20)
                        yield return StartCoroutine(SpawnWaveHuJungbanHard());
                    else if (wave < 999)
                        yield return StartCoroutine(SpawnWaveHuHubanHard());
                    spawnedCount++;
                }
                else
                    yield return null;
            }
        }
        ing = false;
    }
    private IEnumerator SpawnWaveChoban()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[0], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveJungban()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(0, 2);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHuban()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(0, 3);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHuJungban()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(0, 4);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHuHuban()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(0, 5);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveJoke()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[2], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveRich()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[4], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveDont()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[3], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }

    private IEnumerator SpawnWaveChobanHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[5], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveJungbanHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(5, 7);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHubanHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(5, 8);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHuJungbanHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(5, 9);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveHuHubanHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        randomcustomer = Random.Range(5, 10);
        GameObject customer = Instantiate(customerPrefab[randomcustomer], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveJokeHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[7], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveRichHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[9], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }
    private IEnumerator SpawnWaveDontHard()
    {
        yield return new WaitForSeconds(GetSpawnDelay());
        GameObject customer = Instantiate(customerPrefab[8], spawnPoint, Quaternion.identity);
        Customerspawn++;
    }


    private float GetSpawnDelay()
    {
        return Mathf.Clamp(1.5f - wave * 0.05f, 0.1f, 1.5f);
    }
}