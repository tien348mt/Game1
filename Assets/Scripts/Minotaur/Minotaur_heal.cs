using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minotaur_heal : MonoBehaviour
{
    public int startingHealth = 10;

    public int currentHealth;
    private Animator animator;
    public GameObject hp_bar;
    private Minotaur minotaur;
    public Slider heal_bar;
    public PolygonCollider2D colliderPolygon;
    private Minotaur_hurt minotaur_Hurt;
    private PickUpSpawner spawn;
    private SpawnYoi yoi;
    AudioManager audioManager;
    private void Awake()
    {
        minotaur_Hurt = GetComponent<Minotaur_hurt>();
        minotaur = GetComponent<Minotaur>();
        spawn = GetComponent<PickUpSpawner>();
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
        StartCoroutine(minotaur_Hurt.FlashRoutine());
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
    }
    /*public void resetMinotaur()
    {
        currentHealth = startingHealth;
        heal_bar.value = currentHealth;

        collider2D.enabled = true;
        animator.ResetTrigger("die"); 
        minotaur.resetPosition();
        minotaur.player = null;
    }*/
}
