using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] protected int bulletDamage;
    [SerializeField] private float meterGain;

    private float bulletSpeed;
    private Vector3 bulletDirection;
    
    void Update()
    {
        transform.position += bulletDirection * bulletSpeed * Time.deltaTime;
    }

    public void SetBulletParams(float speed, Vector3 direction)
    {
        bulletSpeed = speed;
        bulletDirection = direction;
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    public float GetMeterGain()
    {
        return meterGain;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
