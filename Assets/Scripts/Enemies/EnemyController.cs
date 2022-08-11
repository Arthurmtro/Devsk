using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats enemyStats = new EnemyStats();

    [SerializeField] private int enemyTypeId;

    private Rigidbody2D rigidBody2d;

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();

        // Generate random Enemy type that will define stats
        enemyStats.life = 10;
    }

    // Update is called once per frame
    private void Update()
    {
        if(enemyStats.life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void dealDamage(int damages)
    {
        enemyStats.life -= damages;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullets" || collision.gameObject.name == "Bullet(Clone)")
        {
            rigidBody2d.velocity = Vector3.zero;
            rigidBody2d.angularVelocity = 0f;
        }

    }
}
