using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{

    public GameObject ingameItems;
    public GameObject pausemenu;
    public MouseLook mouseLook;

    void Start()
    {

        pausemenu.SetActive(false);
        ingameItems.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (mouseLook.paused)
            {
                Resume();
            } else
            {
                Pause();
            }

        }

    }

    public void Pause()
    {

        ingameItems.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseLook.paused = true;
        pausemenu.SetActive(true);

    }

    public void Resume()
    {

        ingameItems.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseLook.paused = false;
        pausemenu.SetActive(false);

    }

    public void Exit()
    {

        Application.Quit();

    }

    public void MainMenu()
    {

        SceneManager.LoadScene("Main Menu");

    }

}
