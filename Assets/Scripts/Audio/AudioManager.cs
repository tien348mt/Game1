using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("------------Audio Clip------------")]
    public AudioClip background;
    public AudioClip sword_effect;
    public AudioClip staff_effect;
    public AudioClip death;
    public AudioClip win;
    public AudioClip getCoin;
    public AudioClip hp;
    public AudioClip mana;
    public AudioClip hurt;
    public AudioClip player_dash;
    public AudioClip Wizard_attack;
    public AudioClip Minotaur_attack;
    public AudioClip openChest;

    private static AudioManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void GameOver()
    {
        musicSource.clip = death;
        musicSource.Play();
    }
    public void Win()
    {
        musicSource.clip = win;
        musicSource.Play();
    }
    public void MainMenu()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.PlayOneShot(audioClip);
    }
}
