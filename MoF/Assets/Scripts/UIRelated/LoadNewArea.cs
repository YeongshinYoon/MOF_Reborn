﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string levelToLoad;

    public string exitPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SoundManager.MyInstance.onWarp();
            Player.MyInstance.canMove = false;
            StartCoroutine(TitleScreenControl.MyInstance.LoadingScene(levelToLoad, exitPoint));
        }
    }
}
