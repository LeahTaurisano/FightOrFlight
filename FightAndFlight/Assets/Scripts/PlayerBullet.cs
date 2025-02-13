using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletComponent
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyComponent hitEnemy = collision.gameObject.GetComponent<EnemyComponent>();
            hitEnemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
