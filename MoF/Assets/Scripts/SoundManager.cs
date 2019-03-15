using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour 
{
    private static SoundManager instance;

    public static SoundManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioSource ingameAudio;

    [SerializeField]
    private AudioClip clickLargeButton;

    [SerializeField]
    private AudioClip clickMiddleButton;

    [SerializeField]
    private AudioClip clickSmallButton;

    [SerializeField]
    private AudioClip openWindow;

    [SerializeField]
    private AudioClip closeWindow;

    [SerializeField]
    private AudioClip warp;

    [SerializeField]
    private AudioClip tutorialBGM;

    [SerializeField]
    private AudioClip RibiTownBGM1;

    [SerializeField]
    private AudioClip RibiTownBGM2;

    public bool isIngame;

    public bool SceneLoaded;

    private void Start()
    {
        isIngame = false;
        SceneLoaded = false;
    }

    public void onClickLargeButton()
    {
        audio.clip = clickLargeButton;
        audio.Play();
    }

    public void onClickMiddleButton()
    {
        audio.clip = clickMiddleButton;
        audio.Play();
    }

    public void onClickSmallButton()
    {
        audio.clip = clickSmallButton;
        audio.Play();
    }

    public void onOpenWindow()
    {
        audio.clip = openWindow;
        audio.Play();
    }

    public void onCloseWindow()
    {
        audio.clip = closeWindow;
        audio.Play();
    }

    public void onWarp()
    {
        audio.clip = warp;
        audio.Play();
    }

    public void ingameBGMStop()
    {
        ingameAudio.Stop();
    }

    public void playBGM()
    {
        if (ingameAudio.isPlaying)
        {
            ingameAudio.Stop();
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "Tutorial":
                ingameAudio.clip = tutorialBGM;
                ingameAudio.Play();
                break;

            case "RibiTown_LearningStreet":
                ingameAudio.clip = RibiTownBGM1;
                ingameAudio.Play();
                break;
        }
    }
}
