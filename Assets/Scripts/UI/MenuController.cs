﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject hostItems;
    public GameObject clientItems;
    public GameObject mainItems;
    public GameObject optionsItems;
    public AudioSource audioSrc;

    private float musicVolume = 1f;

    void Start()
    {

        hostItems.SetActive(false);
        clientItems.SetActive(false);
        optionsItems.SetActive(false);

        mainItems.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    void Update()
    {

        audioSrc.volume = musicVolume;

    }


    // Volume control ----------------------------------------------------------

    public void SetVolume(float vol)
    {

        musicVolume = vol;

    }

    // Quality control ---------------------------------------------------------

    public void SetQuality(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    // Misc functions ----------------------------------------------------------

    public void Back()
    {

        hostItems.SetActive(false);
        clientItems.SetActive(false);
        optionsItems.SetActive(false);

        mainItems.SetActive(true);

    }

    public void StartGame()
    {

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            return;
        }

        SceneManager.LoadScene("Hub");

    }

    public void OpenHostItems()
    {

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            return;
        }

        mainItems.SetActive(false);
        hostItems.SetActive(true);

    }

    public void OpenClientItems()
    {

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            return;
        }

        mainItems.SetActive(false);
        clientItems.SetActive(true);

    }

    public void OpenOptionsItems()
    {

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            return;
        }

        mainItems.SetActive(false);
        optionsItems.SetActive(true);

    }

    public void Exit()
    {

        Application.Quit();

    }

}
