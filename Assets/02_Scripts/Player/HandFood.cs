using System.Collections;
using UnityEngine;

public class HandFood : MonoBehaviour
{
    [SerializeField] private GameObject cola;
    [SerializeField] private GameObject fried;
    [SerializeField] private GameObject ham;
    [SerializeField] private GameObject notpatte;
    [SerializeField] private GameObject patte;
    [SerializeField] private GameObject handfool;
    [SerializeField] private GameObject salad;

    public static HandFood Instance = null;
    public int whathand = 0;

    public static bool isTriggered = false;

    private Animator ani;

    private float up;

    private void Awake()
    {
        handfool.SetActive(false);
        ani = GetComponent<Animator>();
        whathand = 0;
        ActiveFalse();

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
        up = ani.GetFloat("runy");
        if (whathand == 0)
            ActiveFalse();
        else if (whathand == 1)
        {
            if (up <= 0)
                cola.SetActive(true);
            else
                ActiveFalse();
        }
        else if (whathand == 2)
        {
            if (up <= 0)
                ham.SetActive(true);
            else
                ActiveFalse();
        }
        else if (whathand == 3)
        {
            if (up <= 0)
                fried.SetActive(true);
            else
                ActiveFalse();
        }
        else if (whathand == 4)
        {
            if (up <= 0)
                salad.SetActive(true);
            else
                ActiveFalse();
        }
        else if (whathand == 5)
        {
            if (up <= 0)
                notpatte.SetActive(true);
            else
                ActiveFalse();
        }
        else if (whathand == 6)
        {
            if (up <= 0)
                patte.SetActive(true);
            else
                ActiveFalse();
        }
    }

    private void ActiveFalse()
    {
        cola.SetActive(false);
        ham.SetActive(false);
        fried.SetActive(false);
        notpatte.SetActive(false);
        salad.SetActive(false);
        patte.SetActive(false);
    }
    public void ChangeFood(int foodnum)
    {
        ActiveFalse();
        whathand = foodnum;
    }

    public IEnumerator HandFool()
    {
        handfool.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        handfool.SetActive(false);
    }

}
