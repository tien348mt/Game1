using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnStaff : MonoBehaviour
{
    [SerializeField] private GameObject satff;
    [SerializeField] private int coinCount = 1;

    public void DropItems()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            Instantiate(satff, transform.position + randomOffset, Quaternion.identity);
        }
    }
}
