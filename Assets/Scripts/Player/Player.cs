using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    private Animator animator;
    [SerializeField] private Minotaur minotaur;
    [SerializeField] private Worm worm;
    [SerializeField] private Ghost ghost;
    [SerializeField] private EvilWizard wizard;
    [SerializeField] private ActiveInventory inventory;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject staff;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private int mana = 100;
    [SerializeField] private Slider manaSlider;
    public GamePause PauseMenu;

    public Text coinText;
    public Text yoiText;

    [HideInInspector] public int coin = 0;
    [HideInInspector] public int staff_item = 0;
    [HideInInspector] public int winGoods = 0;

    public Vector3 MoveInput { get; private set; } 
    public static Player Instance { get; private set; }
    public int manaCurrent { get; private set; }

    public bool canMove = true;
    private bool isDash = false;
    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        manaCurrent = mana;
        StartCoroutine(RecoverMana());
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.gameObject.SetActive(true);
            PauseMenu.PauseGame();
        }
        coinText.text = ": "+ coin.ToString();
        yoiText.text = ": "+ winGoods.ToString();
        Move();
        Dash();
        animator.SetFloat("speed", MoveInput.sqrMagnitude);
        ChangeInventory();
    }

    private void Move()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 direction = (mousePosition - transform.position).normalized;

        if (canMove)
        {
            MoveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += MoveInput * moveSpeed * Time.deltaTime;

            if (mousePosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MinotaurCheckpoint")
        {
            minotaur.ReturnToStartPosition();
        }
        if (collision.gameObject.tag == "WormCheckpoint")
        {
            worm.MoveToStartPosition();
        }
        if (collision.gameObject.tag == "WizardCheckPoint")
        {
            wizard.MoveToStartPosition();
        }
        if (collision.gameObject.tag == "GhostCheckPoint")
        {
            ghost.ReturnToStartPosition();
        }

    }

    private void Dash()
    {
        if (Input.GetMouseButton(1) && !isDash && canMove)
        {
            audioManager.PlaySFX(audioManager.player_dash);
            isDash = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = 1f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDash = false;
    }

    public void Die()
    {
        canMove = false;
        gameObject.SetActive(false);

    }

    private void ChangeInventory()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.ChangeAtive(0);
            sword.SetActive(true);
            staff.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && staff_item == 1)
        {
            inventory.ChangeAtive(1);
            staff.SetActive(true);
            sword.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioManager.PlaySFX(audioManager.hp);
            inventory.ChangeAtive(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioManager.PlaySFX(audioManager.mana);
            inventory.ChangeAtive(3);
            
            manaCurrent += 20;
            manaCurrent = Mathf.Min(manaCurrent, mana); 
            manaSlider.value = manaCurrent;
        }
    }


    public void ReduceMana(int amount)
    {
        manaCurrent -= amount;
        manaCurrent = Mathf.Max(manaCurrent, 0);
        manaSlider.value = manaCurrent;
    }

    private IEnumerator RecoverMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (manaCurrent < mana)
            {
                manaCurrent += 2;
                manaCurrent = Mathf.Min(manaCurrent, mana);
                manaSlider.value = manaCurrent;
            }
        }
    }
}
