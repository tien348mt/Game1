using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_evil : MonoBehaviour
{
    public PolygonCollider2D attackCollider;

   public void Attack()
    {
        attackCollider.enabled = true;
    }
    public void EndAttack()
    {
        attackCollider.enabled = false;
    }
    public void DestroyAttack()
    {
        Destroy(gameObject);
    }
    
}
