using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preload : MonoBehaviour
{
    private static preload instance;

    public static preload MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<preload>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GameObject UICanvas;

    public GameObject MyUICanvas
    {
        get
        {
            return UICanvas;
        }
    }

    [SerializeField]
    private GameObject DamageCanvas;

    public GameObject MyDamageCanvas
    {
        get
        {
            return DamageCanvas;
        }
    }

    [SerializeField]
    private GameObject SaveGame;

    public GameObject MySaveGame
    {
        get
        {
            return SaveGame;
        }
    }

    [SerializeField]
    private GameObject MainMenu;

    public GameObject MyMainMenu
    {
        get
        {
            return MainMenu;
        }
    }

    [SerializeField]
    private GameObject Camera;

    public GameObject MyCamera
    {
        get
        { 
            return Camera; 
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ONOFF(bool trigger)
    {
        UICanvas.SetActive(trigger);
        DamageCanvas.SetActive(trigger);
        MainMenu.SetActive(trigger);
        SaveGame.SetActive(trigger);
    }
}
