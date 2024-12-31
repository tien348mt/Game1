using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private void Start()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Player.Instance.staff_item == 1)
        {
            this.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }
    public void ChangeAtive(int index)
    {
        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
    }
}
