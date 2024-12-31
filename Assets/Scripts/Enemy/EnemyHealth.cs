using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    [HideInInspector] public int currentHealth { get; private set; }
    public Slider hp_bar;
    private KnockBack knockBack;
    private Flash flash;
    private Animator animator;
    public PolygonCollider2D collider2d;
    private PickUpSpawner spawner;
    AudioManager audioManager;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        animator = GetComponent<Animator>();
        spawner = GetComponent<PickUpSpawner>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start() {

        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        audioManager.PlaySFX(audioManager.hurt);
        currentHealth -= damage;
        knockBack.GetKnockedBack(Player.Instance.transform, 2f);
        StartCoroutine(flash.FlashRoutine());
        //DetectDeath();
        hp_bar.value = currentHealth;
    }

    public void DetectDeath() {
        if (currentHealth <= 0) {
            animator.SetTrigger("die");
            hp_bar.enabled = false;
        }
    }
    public void Des()
    {
        gameObject.gameObject.SetActive(false);
        spawner.DropItems();
    }
    public void startDead()
    {
        collider2d.enabled = false;
    }
}
