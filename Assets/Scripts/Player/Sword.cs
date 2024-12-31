using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private GameObject swordCollider;

    private GameObject slashAnim;
    private bool canAttack = true;
    [SerializeField] private float attackCooldown = 0.5f;
    private float lastAttackTime;
    AudioManager audioManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            audioManager.PlaySFX(audioManager.sword_effect);
            Attack();
            swordCollider.gameObject.SetActive(true);
            canAttack = false;
            lastAttackTime = Time.time;
        }

        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true; 
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SlashDown()
    {
        if (transform.localScale.x < 0)
        {
            slashAnim.transform.localScale = new Vector3(-1, 1, 0);
        }
        else
        {
            slashAnim.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    public void SlashUp()
    {
        if (transform.localScale.x < 0)
        {
            slashAnim.transform.localScale = new Vector3(-1, -1, 0);
        }
        else
        {
            slashAnim.transform.localScale = new Vector3(1, -1, 0);
        }
    }

    public void notAttack()
    {
        swordCollider.gameObject.SetActive(false);
    }
}
