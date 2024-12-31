using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<EnemyHealth>()) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
        else if (other.gameObject.GetComponent<Minotaur_heal>())
        {
            Minotaur_heal enemyHealth = other.gameObject.GetComponent<Minotaur_heal>();
            enemyHealth.TakeDamage(damageAmount);
        }else if (other.gameObject.GetComponent<Worm_heal>())
        {
            Worm_heal enemyHealth = other.gameObject.GetComponent<Worm_heal>();
            enemyHealth.TakeDamage(damageAmount);
        }
        else if (other.gameObject.GetComponent<EvilWizard_heal>())
        {
            EvilWizard_heal enemyHealth = other.gameObject.GetComponent<EvilWizard_heal>();
            enemyHealth.TakeDamage(damageAmount);
        }
        else if (other.gameObject.GetComponent<Ghost_heal>())
        {
            Ghost_heal enemyHealth = other.gameObject.GetComponent<Ghost_heal>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
