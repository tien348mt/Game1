using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Ghost_heal heal;
    public GameObject hp_bar;
    private bool returnToStart = false;
    public float zone; // Tầm bắn của quái
    private GameObject player;
    public Shooter shooter;
   
    private void Awake()
    {
        heal = GetComponent<Ghost_heal>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Player_Health.Instance.currentHealth <= 0)
        {
            ReturnToStartPosition();
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (returnToStart)
        {
            hp_bar.gameObject.SetActive(false);
            shooter.isShooting = false;
            returnToStart = false;
        }
        else if (distanceToPlayer < zone)
        {
            hp_bar.gameObject.SetActive(true);

            shooter.isShooting = true;
        }
    }

    public void ReturnToStartPosition()
    {
        returnToStart = true;
        heal.currentHealth = heal.startingHealth;
        heal.heal_bar.value = heal.startingHealth;
    }
}
