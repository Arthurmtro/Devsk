using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rigidBody2d;

    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<CapsuleCollider2D>());
    }

    private void FixedUpdate()
    {
        transform.position -= transform.up * Time.deltaTime * speed;
        rigidBody2d.MovePosition(rigidBody2d.position * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
