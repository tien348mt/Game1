using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost_heal : MonoBehaviour
{
    public int startingHealth = 10;

    public int currentHealth;
    private Animator animator;
    public GameObject hp_bar;
    public Slider heal_bar;
    public PolygonCollider2D colliderPolygon;
    private Ghots_hurt hurt;
    private PickUpSpawner spawn;
    private SpawnYoi yoi;
    AudioManager audioManager;
    private void Awake()
    {
        hurt = GetComponent<Ghots_hurt>();
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
        StartCoroutine(hurt.FlashRoutine());
        heal_bar.value = currentHealth;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Des();

        }
    }
    public void Des()
    {
        gameObject.gameObject.SetActive(false);
        hp_bar.gameObject.SetActive(false);
        yoi.DropItems();
        spawn.DropItems();
    }
}
