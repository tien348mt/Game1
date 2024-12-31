using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staff : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject staff_effect_prefab;
    [SerializeField] private Transform staff_effect_point;
    private GameObject staff_effect_Anim;
    [SerializeField] private GameObject fire_prefab;
    private GameObject fire;
    private bool canAttack = true;
    [SerializeField] private float attackCooldown = 0.4f;
    private float lastAttackTime;
    AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack && Player.Instance.manaCurrent >= 5)
        {
            Attack();
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
        audioManager.PlaySFX(audioManager.staff_effect);
        animator.SetTrigger("attack");
        staff_effect_Anim = Instantiate(staff_effect_prefab, staff_effect_point.position, Quaternion.identity);
        staff_effect_Anim.transform.parent = this.transform.parent;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        fire = Instantiate(fire_prefab, worldPosition, Quaternion.identity);
        Player.Instance.ReduceMana(20);
    }
}
