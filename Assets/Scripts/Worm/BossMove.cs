using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float zone;
    public float stoppingDistance = 1f;
    public GameObject hp_bar;

    // Biến dùng cho tấn công
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float fireSpeed = 3f;
    public float attackInterval = 2f;

    private Vector2 startPosition;
    private Animator animator;
    private bool isFacingRight = true;
    private bool returnToStart = false;
    public bool isAttacking = false;

    private Worm_heal worm_Heal;
    AudioManager audioManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        worm_Heal = GetComponent<Worm_heal>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Player_Health.Instance.currentHealth <= 0)
        {
            MoveToStartPosition();
        }
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
       
        if (returnToStart)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            hp_bar.gameObject.SetActive(false);
            animator.SetFloat("run", 1);

            if (Vector2.Distance(transform.position, startPosition) < 0.1f)
            {
                animator.SetFloat("run", 0);
                returnToStart = false;
            }

            Flip((startPosition - (Vector2)transform.position).x);
        }
        else if (distanceToPlayer < zone)
        {
            hp_bar.gameObject.SetActive(true);

            if (distanceToPlayer > stoppingDistance)
            {
                MoveTowardsPlayer();
            }
            if(distanceToPlayer <= stoppingDistance)
            {
                animator.SetFloat("run", 0);
                if (!isAttacking)
                {
                    Flip((player.transform.position - transform.position).x);
                    animator.SetTrigger("attack");
                }
            }
        }
        else
        {
            MoveToStartPosition();
        }
    }


    public void MoveToStartPosition()
    {
        returnToStart = true;
        worm_Heal.currentHealth = worm_Heal.startingHealth;
        worm_Heal.heal_bar.value = worm_Heal.startingHealth;
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetFloat("run", 1);
        Flip((player.transform.position - transform.position).x);
    }
    public void AttackPlayer()
    {
        isAttacking = true;
        ShootBullet();
    }
    public IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            audioManager.PlaySFX(audioManager.staff_effect);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);


            Vector2 shootDirection = (player.transform.position - bulletSpawnPoint.position).normalized;


            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);


            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * fireSpeed;
            }
        }
    }

    void Flip(float directionX)
    {
        if ((directionX > 0 && !isFacingRight) || (directionX < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
