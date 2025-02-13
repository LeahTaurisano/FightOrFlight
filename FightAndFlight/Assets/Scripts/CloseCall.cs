using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCall : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            if (collision.isActiveAndEnabled)
            {
                BulletComponent closeBullet = collision.GetComponent<BulletComponent>();
                player.GainMeter(closeBullet.GetMeterGain());
            }
        }
    }
}
