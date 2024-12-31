using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flydemon : MonoBehaviour
{
    private GameObject player;
    public float chaseRadius = 2f;
    public float moveSpeed = 2f;
    private Vector3 initialPosition;
    private bool isChasing = false;
    private bool facingRight = true;

    public bool showChaseRadius = true;
    private LineRenderer lineRenderer;

    private float stopChaseDistance = 1f;
    private bool isAttacking = false;
    public float attackInterval = 2f;

    public Transform bulletSpawnPoint; 
    public GameObject bulletPrefab;
    public float fireSpeed = 3f;

    public EnemyHealth enemyHealth;

    void Start()
    {
        initialPosition = transform.position;
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 50;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        player = GameObject.FindGameObjectWithTag("Player");
        DrawCircle();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        lineRenderer.enabled = showChaseRadius;

        if (distanceToPlayer <= chaseRadius)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                StartCoroutine(ReturnToStartPosition());
            }
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > stopChaseDistance)
        {
            if ((player.transform.position.x > transform.position.x && facingRight) ||
                (player.transform.position.x < transform.position.x && !facingRight))
            {
                Flip();
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (!isAttacking)
        {
            if (enemyHealth.currentHealth <= 0)
            {
                return;
            }
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        ShootBullet();

        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

          
            Vector2 shootDirection = (player.transform.position - bulletSpawnPoint.position).normalized;

            
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle +180f);

          
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * fireSpeed;
            }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator ReturnToStartPosition()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            if ((initialPosition.x > transform.position.x && facingRight) ||
                (initialPosition.x < transform.position.x && !facingRight))
            {
                Flip();
            }

            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isAttacking = false;
    }

    void DrawCircle()
    {
        float angleStep = 360f / lineRenderer.positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * chaseRadius;
            float z = Mathf.Sin(angle) * chaseRadius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }
}
