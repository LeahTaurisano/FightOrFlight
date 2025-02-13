using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float moveSpeed;
    private GameObject player;
    private SpriteRenderer enemySprite;
    private ShootAtPlayer shootComponent;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySprite = GetComponent<SpriteRenderer>();
        shootComponent = GetComponent<ShootAtPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 normalizedDirection = Vector3.Normalize(directionToPlayer);
        shootComponent.Shoot(normalizedDirection);
        if (directionToPlayer.sqrMagnitude > distanceFromPlayer * distanceFromPlayer)
        {
            //rb.MovePosition(rb.position + new Vector2(normalizedDirection.x, normalizedDirection.y) * moveSpeed * Time.deltaTime);
            transform.position += normalizedDirection * moveSpeed * Time.deltaTime;
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemySprite.color = Color.grey;
        Invoke("ResetColor", 0.05f);
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void ResetColor()
    {
        enemySprite.color = Color.red;
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {

    }
}
