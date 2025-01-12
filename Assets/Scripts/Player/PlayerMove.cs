using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private readonly int hashMove = Animator.StringToHash("Move");

    public float speed = 5f;

    private Animator animator;
    private Rigidbody rb;
    private PlayerInput input;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        var pos = transform.position;
        pos += input.Move * speed * Time.deltaTime;
        rb.MovePosition(pos);
        rb.velocity = Vector3.zero;

        var look = input.MousePosition - transform.position;

        rb.MoveRotation(Quaternion.LookRotation(look, Vector3.up));

        animator.SetBool(hashMove, input.Move.magnitude > 0f);
    }
}
