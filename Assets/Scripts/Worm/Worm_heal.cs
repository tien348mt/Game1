using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worm_heal : MonoBehaviour
{
    public int startingHealth = 10;

    public int currentHealth;
    private Animator animator;
    public GameObject hp_bar;
    private Worm worm;
    public Slider heal_bar;
    public PolygonCollider2D colliderPolygon;
    private Worm_hurt worm_Hurt;
    private PickUpSpawner spawnSpawner;
    private SpawnYoi yoi;
    AudioManager audioManager;
    private void Awake()
    {
        worm_Hurt = GetComponent<Worm_hurt>();
        worm = GetComponent<Worm>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        yoi = GetComponent<SpawnYoi>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spawnSpawner = GetComponent<PickUpSpawner>();
        
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.hurt);
        currentHealth -= damage;
        StartCoroutine(worm_Hurt.FlashRoutine());
        heal_bar.value = currentHealth;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            colliderPolygon.enabled = false;
            animator.SetTrigger("die");
            worm.isAttacking = true;
        }
    }
    public void Des()
    {
        spawnSpawner.DropItems();
        yoi.DropItems();
        gameObject.gameObject.SetActive(false);
        hp_bar.gameObject.SetActive(false);
    }
}
