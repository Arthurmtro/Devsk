using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Animator animator;


    private Vector2 movement;

    [SerializeField] private float moveSpeed = 5f;



    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("animator is not assigned !");
        }

        if (rigidBody2d == null)
        {
            Debug.LogError("rigidBody2d is not assigned !");
        }
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0 && transform.localScale.x != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else if (movement.x >= 0 && transform.localScale.x == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rigidBody2d.MovePosition(rigidBody2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
