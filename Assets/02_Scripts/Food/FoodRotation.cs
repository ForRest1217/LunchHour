using UnityEngine;

public class FoodRotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Animator ani;
    private float runx;
    private float runy;
    private Vector3 up;

    private void Awake()
    {
        ani = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        runx = ani.GetFloat("runx"); //ad
        runy = ani.GetFloat("runy"); // ws
        if (runx < -0.5f && runy == 0)
        {
            if (gameObject.tag == "Patte")
            {
                up = new Vector3(-0.5f, -0.934f, 0);
                SetLeft();
            }
            else if (gameObject.tag == "Hamburger")
            {
                up = new Vector3(-0.5f, -0.86f, 0);
                SetLeft();
            }
            else
            {
                up = new Vector3(-0.5f, -0.64f, 0);
                SetLeft();
            }
        }
        else if (runy < -0.5f || runx == 0 && runy == 0)
        {
            if (gameObject.tag == "Patte")
            {
                up = new Vector3(0, -0.934f, 0);
                SetLeft();
            }
            else if (gameObject.tag == "Hamburger")
            {
                up = new Vector3(0, -0.86f, 0);
                SetLeft();
            }
            else
            {
                up = new Vector3(0, -0.64f, 0);
                SetLeft();
            }
        }
        else if (runx > 0.5f)
        {
            if (gameObject.tag == "Patte")
            {
                up = new Vector3(0.5f, -0.934f, 0);
                SetLeft();
            }
            else if (gameObject.tag == "Hamburger")
            {
                up = new Vector3(0.5f, -0.86f, 0);
                SetLeft();
            }
            else
            {
                up = new Vector3(0.5f, -0.64f, 0);
                SetLeft();
            }
        }
    }

    private void SetLeft()
    {
        gameObject.transform.position = player.position + up;
    }
}
