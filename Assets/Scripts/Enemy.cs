using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth = 3;
    public float moveSpeed = 5.5f;
    public Transform playerLocation;
    public int damage = 1;
    public EnemyManager enemyManager;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            enemyManager.numEnemies -= 1;
        }
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
            player.TakeDamage(damage);
    }*/

    public void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
