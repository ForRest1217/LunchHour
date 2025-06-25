using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private readonly int moveXhash = Animator.StringToHash("runx"); //읽기전용 runx 애니메이터
    private readonly int moveYhash = Animator.StringToHash("runy"); //읽기전용 runy 애니메이터
    private Rigidbody2D rigid;
    private Animator ani;

    private Vector2 moveDir;
    private float speed = 5;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rigid.linearVelocity = moveDir * speed;
    }

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        PlayerAnimator();
    }

    private void PlayerAnimator()
    {
        ani.SetFloat(moveXhash, moveDir.x);
        ani.SetFloat(moveYhash, moveDir.y);
    }

}

