using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool hasCollided = false; // Used to know if it can still deal damages

    [SerializeField] private float thrust = 10f;

    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rigidBody2d;
    private GameObject player;
    private int bulletLifeTime = 8;

    void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigidBody2d = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), capsuleCollider2D);

        rigidBody2d.AddForce(-transform.up * thrust, ForceMode2D.Force);

        StartCoroutine(destroyBullet());
    }

    private void Update()
    {
        if (rigidBody2d.velocity.magnitude >= 0.3f && hasCollided && Physics2D.GetIgnoreCollision(player.GetComponent<BoxCollider2D>(), capsuleCollider2D))
        {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), capsuleCollider2D, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerWeapon>().addAmmo(1);
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "Player Bullets" || collision.gameObject.name == "Bullet(Clone)" || hasCollided)
        {
            hasCollided = true;
            Physics2D.IgnoreCollision(collision.collider, capsuleCollider2D);
            return;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            hasCollided = true;
            collision.gameObject.GetComponent<EnemyController>().dealDamage(0);
        }

        if (!hasCollided)
        {
            hasCollided = true;
        }
        else
        {
            rigidBody2d.velocity = Vector3.zero;
        }

        rigidBody2d.angularVelocity = 0f;
    }

    private IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
 
}
