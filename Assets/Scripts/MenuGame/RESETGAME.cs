using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RESETGAME : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void RestartGame()
    {
        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManager.MainMenu();
    }
    public void MainMenu()
    {
        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(0);
        audioManager.MainMenu();
    }
}
