using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text coin;
    public Text win;
    public GameObject WinGame;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        coin.text = Player.Instance.coin.ToString()+ "/150";
        win.text = Player.Instance.winGoods.ToString()+ "/4";
    }
    public void accept()
    {
        if(Player.Instance.coin >=150 && Player.Instance.winGoods == 4)
        {
            gameObject.SetActive(false);
            WinGame.SetActive(true);
            audioManager.Win();

        }
    }
    public void denny()
    {
        gameObject.SetActive(false);
    }
}
