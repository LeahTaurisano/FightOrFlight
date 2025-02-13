using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootTimer;
    [SerializeField] private float bulletSpeed;

    private EnemyComponent enemyComponent;
    private float currentTimer = 0;

    private void Start()
    {
        enemyComponent = GetComponent<EnemyComponent>();
    }

    public void Shoot(Vector3 directionToPlayer)
    {
        if (enemyComponent.GetPlayer() != null)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > shootTimer)
            {
                currentTimer = 0;
                GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                BulletComponent spawnedComponent = spawnedBullet.GetComponent<BulletComponent>();
                spawnedComponent.SetBulletParams(bulletSpeed, directionToPlayer);
            }
        }
    }
}
