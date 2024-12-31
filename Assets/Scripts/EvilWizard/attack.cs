using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; // Số sát thương
    [SerializeField] private float damageInterval = 1f; // Khoảng thời gian giữa các lần gây sát thương
    private bool isPlayerInRange = false; // Trạng thái kiểm tra người chơi trong vùng
    private Player_Health player_Health; // Lưu trữ tham chiếu đến Player_Health
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player_Health>())
        {
            isPlayerInRange = true;
            player_Health = other.gameObject.GetComponent<Player_Health>();
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayerOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player_Health>())
        {
            isPlayerInRange = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamagePlayerOverTime()
    {
        while (isPlayerInRange)
        {
            player_Health.TakeDamage(damageAmount); // Gây sát thương
            yield return new WaitForSeconds(damageInterval); // Chờ khoảng thời gian
        }
    }
}
