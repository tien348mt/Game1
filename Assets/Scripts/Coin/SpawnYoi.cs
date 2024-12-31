using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnYoi : MonoBehaviour
{
    [SerializeField] private GameObject yoi;
    [SerializeField] private int yoiCount = 1;
    public void DropItems()
    {
        for (int i = 0; i < yoiCount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            Instantiate(yoi, transform.position + randomOffset, Quaternion.identity);
        }
    }
}
