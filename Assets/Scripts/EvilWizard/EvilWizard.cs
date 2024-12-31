using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonoBehaviour
{
    private Animator animator;
    public GameObject weaponCollider;
    public PolygonCollider2D enemyCollider;

    public GameObject player;
    public float speed;
    public float zone;
    public float stoppingDistance = 1f;
    public GameObject hp_bar;

    private Vector2 startPosition;
    private bool isFacingRight = true;
    private bool returnToStart = false;
    private bool isAttacking = false;

    public float attackCooldown = 3f;
    private float lastAttackTime = -Mathf.Infinity;

    public GameObject prefabToSpawn;
    public float spawnInterval = 2f; 
    private bool isPlayerInZone = false;

    private EvilWizard_heal heal;
    AudioManager audioManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        heal = GetComponent<EvilWizard_heal>();
        StartCoroutine(SpawnObjectWhenPlayerInZone());
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
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        isPlayerInZone = distanceToPlayer < zone;

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
        else if (isPlayerInZone)
        {
            hp_bar.gameObject.SetActive(true);
            if (distanceToPlayer > stoppingDistance)
            {
                MoveTowardsPlayer();
            }
            else if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetFloat("run", 0);
                Flip((player.transform.position - transform.position).x);

                // Bắt đầu tấn công
                animator.SetTrigger("attack");
                isAttacking = true;
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
        heal.currentHealth = heal.startingHealth;
        heal.heal_bar.value = heal.startingHealth;

    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetFloat("run", 1);
        Flip((player.transform.position - transform.position).x);
    }

    void Flip(float directionX)
    {
        if ((directionX > 0 && !isFacingRight) || (directionX < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void WeaponCollider()
    {
        weaponCollider.gameObject.SetActive(false);
        isAttacking = false;
        lastAttackTime = Time.time;
    }

    public void Attacking()
    {
        audioManager.PlaySFX(audioManager.Wizard_attack);
        weaponCollider.gameObject.SetActive(true);
    }

    public void preAttack()
    {
        audioManager.PlaySFX(audioManager.staff_effect);
        enemyCollider.enabled = false;
    }

    public void startAttack()
    {
        enemyCollider.enabled = true;
    }

    IEnumerator SpawnObjectWhenPlayerInZone()
    {
        while (true)
        {
            if (isPlayerInZone && player != null && prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, player.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
