using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flydemon_fire : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
