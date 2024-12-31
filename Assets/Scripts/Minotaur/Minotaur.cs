using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float zone;
    public GameObject hp_bar;
    public GameObject weaponCollider;
    public Minotaur_heal minotaur_Heal;

    private Vector2 startPosition;
    private Animator animator;

    private bool isFacingRight = true;
    private bool returnToStart = false;

    private int attackType = 0; // Biến để luân phiên giữa hai loại tấn công
    private float attackCooldown = 1.5f; // Thời gian chờ giữa mỗi lần tấn công
    private float attackCooldownTimer = 0f; // Bộ đếm thời gian cooldown cho đòn tấn công

    private float teleportCooldown = 10f; // Thời gian cooldown cho teleport
    private float teleportTimer = 0f; // Thời gian đã trôi qua
    private bool isTeleporting = false; // Biến để kiểm tra nếu đang thực hiện teleport

    private bool hasDetectedPlayer = false; // Biến để xác định xem Minotaur đã phát hiện người chơi
    private float detectPlayerTime = 2f; // Thời gian chờ trước khi có thể teleport sau khi phát hiện người chơi
    private float detectPlayerTimer = 0f; // Bộ đếm thời gian từ khi phát hiện người chơi
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        
    }
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Player_Health.Instance.currentHealth <= 0)
        {
            ReturnToStartPosition();
        }
        teleportTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime; // Cập nhật thời gian cooldown của đòn đánh

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

            if (!hasDetectedPlayer)
            {
                hasDetectedPlayer = true;
                detectPlayerTimer = 0f;
            }
            else
            {
                detectPlayerTimer += Time.deltaTime;
            }

            float stoppingDistance = 1f;

            if (teleportTimer >= teleportCooldown && detectPlayerTimer >= detectPlayerTime)
            {
                TeleportAndAttack();
                teleportTimer = 0f;
            }
            else if (distanceToPlayer > stoppingDistance && !isTeleporting)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetFloat("run", 1);
            }
            else if (!isTeleporting && attackCooldownTimer >= attackCooldown) // Kiểm tra nếu đủ thời gian cooldown để thực hiện đòn đánh
            {
                attackCooldownTimer = 0f; // Reset lại bộ đếm thời gian cooldown
                PerformAttack();
            }

            Flip((player.transform.position - transform.position).x);
        }
        else
        {
            animator.SetFloat("run", 0);
            hasDetectedPlayer = false;
        }
    }

    public void ReturnToStartPosition()
    {
        returnToStart = true;
        minotaur_Heal.currentHealth = minotaur_Heal.startingHealth;
        minotaur_Heal.heal_bar.value = minotaur_Heal.startingHealth;
    }

    void Flip(float directionX)
    {
        if (directionX > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(4, 4, 0);
        }
        else if (directionX < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-4, 4, 0);
        }
    }

    void PerformAttack()
    {
        if (!isTeleporting)
        {
            if (attackType == 0)
            {
                animator.SetTrigger("attack");
            }
            else
            {
                animator.SetTrigger("attack2");
            }

            // Đổi loại tấn công sau mỗi lần thực hiện đòn đánh
            attackType = 1 - attackType;
        }
    }

    void TeleportAndAttack()
    {
        isTeleporting = true;
        Vector2 teleportPosition = new Vector2(player.transform.position.x - 1f, player.transform.position.y);

        transform.position = teleportPosition;
        animator.SetTrigger("teleAttack");

        StartCoroutine(ResetTeleporting());
    }

    IEnumerator ResetTeleporting()
    {
        yield return new WaitForSeconds(1f);
        isTeleporting = false;
    }

    public void WeaponCollider()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    public void Attacking()
    {
        audioManager.PlaySFX(audioManager.Minotaur_attack);
        weaponCollider.gameObject.SetActive(true);
    }

   /* public void resetPosition()
    {
        transform.position = startPosition;
        isFacingRight = true;
        transform.localScale = new Vector3(4, 4, 0);

        animator.SetFloat("run", 0);
        attackCooldownTimer = 0f;
        teleportTimer = 0f;
        hasDetectedPlayer = false;
        isTeleporting = false;
        if (hp_bar != null)
        {
            hp_bar.gameObject.SetActive(true);
        }

        
        player = null;

       
        if (weaponCollider != null)
        {
            weaponCollider.gameObject.SetActive(false);
        }
    }*/
}
