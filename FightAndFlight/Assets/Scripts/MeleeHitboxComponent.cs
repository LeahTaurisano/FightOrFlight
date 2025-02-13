using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitboxComponent : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance;
    [SerializeField] private int damage;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = Vector3.Normalize(mousePos - player.transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.position = player.transform.position + direction * distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyComponent hitEnemy = collision.GetComponent<EnemyComponent>();
            hitEnemy.TakeDamage(damage);
        }
    }
}
