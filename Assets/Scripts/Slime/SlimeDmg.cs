using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDmg : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    public TimetoDmg timetoDmg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player_Health>())
        {
            Player_Health player_Health = other.gameObject.GetComponent<Player_Health>();
            player_Health.TakeDamage(damageAmount);
            timetoDmg.DisableCollider();
        }
    }
}
