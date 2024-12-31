using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    public static Player_Health Instance;
    public int currentHealth;
    private Animator animator;
    public GameObject hp_bar;
    public Slider heal_bar;
    private Player_hurt player_Hurt;
    public PolygonCollider2D playerCollider;
    [SerializeField] private GameObject Over;
    AudioManager audioManager;
    private void Awake()
    {
        player_Hurt = GetComponent<Player_hurt>();
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentHealth += 2;
            heal_bar.value = currentHealth;
        }
        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.hurt);
        currentHealth -= damage;
        StartCoroutine(player_Hurt.FlashRoutine());
        heal_bar.value = currentHealth;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("die");
            playerCollider.enabled = false;
            audioManager.GameOver();
            Over.gameObject.SetActive(true);
        }
    }
    public void Des()
    {
        Destroy(gameObject);
        Destroy(hp_bar);
    }
}
