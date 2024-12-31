using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilWizard_heal : MonoBehaviour
{
    public int startingHealth = 10;

    public int currentHealth;
    private Animator animator;
    public GameObject hp_bar;
    public Slider heal_bar;
    public PolygonCollider2D colliderPolygon;
    private EvilWizard_hurt hurt;
    private PickUpSpawner spawn;
    private SpawnYoi yoi;
    private PickUpSpawnStaff spawnStaff;
    AudioManager audioManager;
    private void Awake()
    {
        spawn = GetComponent<PickUpSpawner>();
        hurt = GetComponent<EvilWizard_hurt>();
        spawnStaff = GetComponent<PickUpSpawnStaff>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        yoi = GetComponent<SpawnYoi>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.hurt);
        currentHealth -= damage;
        StartCoroutine(hurt.FlashRoutine());
        heal_bar.value = currentHealth;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            colliderPolygon.enabled = false;
            animator.SetTrigger("die");

        }
    }
    public void Des()
    {
        gameObject.gameObject.SetActive(false);
        hp_bar.gameObject.SetActive(false);
        spawn.DropItems(); 
        yoi.DropItems();
        spawnStaff.DropItems();
    }
}
