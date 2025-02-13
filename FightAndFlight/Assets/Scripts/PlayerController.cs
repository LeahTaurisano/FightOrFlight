using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseDashTime;
    [SerializeField] private float powerMoveSpeedMod;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootTimer;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject fairy;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite baseBackSprite;
    [SerializeField] private Sprite powerSprite;
    [SerializeField] private Sprite powerBackSprite;
    [SerializeField] private float meterDrainRate;

    private Rigidbody2D rb;
    private float moveSpeed;
    private float dashTime;
    private float currentTimer = 0;
    private float dashMod = 1.0f;
    private bool isDashing = false;
    private Vector3 dashVector;
    private bool powerMode = false;
    private SpriteRenderer playerSprite;

    private float maxMeter = 100.0f;
    private float currentMeter = 0.0f;

    private void Start()
    {
        moveSpeed = baseMoveSpeed;
        dashTime = baseDashTime;
        currentTimer = shootTimer + 1.0f;
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (powerMode)
        {
            currentMeter -= (maxMeter / meterDrainRate) * Time.deltaTime;
        }
        if (!isDashing)
        {
            Move(moveInput, mousePos);
        }
        else
        {
            Move(dashVector, mousePos);
        }

        if (currentTimer < shootTimer)
        {
            currentTimer += Time.deltaTime;
        }
        if (!powerMode)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            MeleeSwing();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(moveInput);
        }
        if (Mathf.Approximately(currentMeter, maxMeter) && !powerMode)
        {
            SwitchMode(mousePos);
        }
        else if (currentMeter < 0 && powerMode)
        {
            currentMeter = 0;
            SwitchMode(mousePos);
        }
    }

    private void ResetDashMod()
    {
        dashMod = 1.0f;
        isDashing = false;
        dashVector = Vector3.zero;
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            BulletComponent hitBullet = collision.gameObject.GetComponent<BulletComponent>();
            TakeDamage(hitBullet.GetBulletDamage());
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move(Vector3 direction, Vector3 mousePos)
    {
        transform.position += Vector3.Normalize(direction) * moveSpeed * dashMod * Time.deltaTime;
        if (!Mathf.Approximately(direction.x, 0.0f))
        {
            if (direction.x < 0)
            {
                playerSprite.flipX = true;
            }
            else
            {
                playerSprite.flipX = false;
            }
        }
        if (!Mathf.Approximately(direction.y, 0.0f))
        {
            if (direction.y < 0)
            {
                if (!powerMode)
                {
                    playerSprite.sprite = baseSprite;
                }
                else
                {
                    playerSprite.sprite = powerSprite;
                }
            }
            else
            {
                if (!powerMode)
                {
                    playerSprite.sprite = baseBackSprite;
                }
                else
                {
                    playerSprite.sprite = powerBackSprite;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            
            if (mousePos.x < transform.position.x)
            {
                playerSprite.flipX = true;
            }
            else
            {
                playerSprite.flipX = false;

            }
            if (mousePos.y < transform.position.y)
            {
                if (!powerMode)
                {
                    playerSprite.sprite = baseSprite;
                }
                else
                {
                    playerSprite.sprite = powerSprite;
                }
            }
            else
            {
                if (!powerMode)
                {
                    playerSprite.sprite = baseBackSprite;
                }
                else
                {
                    playerSprite.sprite = powerBackSprite;
                }
            }
        }
    }

    private void Shoot()
    {
        if (currentTimer > shootTimer)
        {
            currentTimer = 0;
            GameObject spawnedBullet = Instantiate(bullet, fairy.transform.position, Quaternion.identity);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 directionToPlayer = Vector3.Normalize(mousePos - transform.position);
            BulletComponent spawnedComponent = spawnedBullet.GetComponent<BulletComponent>();
            spawnedComponent.SetBulletParams(bulletSpeed, directionToPlayer);
        }
    }

    private void Dash(Vector3 direction)
    {
        if (!isDashing)
        {
            dashMod = moveSpeed * 2;
            isDashing = true;
            dashVector = direction;
            Invoke("ResetDashMod", dashTime);
        }
    }

    private void SwitchMode(Vector3 mousePos)
    {
        powerMode = !powerMode;
        fairy.SetActive(!fairy.activeSelf);
        if (powerMode)
        {
            if (mousePos.y < 0)
            {
                playerSprite.sprite = powerBackSprite;
            }
            else
            {
                playerSprite.sprite = powerSprite;
            }
            moveSpeed *= powerMoveSpeedMod;
            dashTime /= (1.5f * powerMoveSpeedMod);
        }
        else
        {
            playerSprite.sprite = baseSprite;
            moveSpeed = baseMoveSpeed;
            dashTime = baseDashTime;
        }
    }

    private void MeleeSwing()
    {
        FlipMeleeHitbox();
        Invoke("FlipMeleeHitbox", 0.15f);
    }

    private void FlipMeleeHitbox()
    {
        meleeHitbox.SetActive(!meleeHitbox.activeSelf);
    }

    public void GainMeter(float num)
    {
        currentMeter += num;
    }

    public float GetMeter()
    {
        return currentMeter;
    }
}
