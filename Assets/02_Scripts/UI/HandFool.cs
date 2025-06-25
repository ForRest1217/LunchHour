using UnityEngine;

public class HandFool : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.Play("Handfool");
    }
}
