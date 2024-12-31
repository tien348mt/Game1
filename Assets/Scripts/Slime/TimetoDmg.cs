using System.Collections;
using UnityEngine;

public class TimetoDmg : MonoBehaviour
{
    private float time = 1f;
    public PolygonCollider2D polygon;

    void Start()
    {
        StartCoroutine(EnableColliderRoutine());
    }

    IEnumerator EnableColliderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(time); 
            polygon.enabled = true;                
        }
    }

    public void DisableCollider()
    {
        polygon.enabled = false;
    }
}
