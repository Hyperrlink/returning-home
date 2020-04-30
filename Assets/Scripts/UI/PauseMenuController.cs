using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PauseMenuController : NetworkBehaviour
{

    public GameObject ingameItems;
    public GameObject pausemenu;
    public MouseLook mouseLook;

    public Text player1Name;
    public Text player2Name;

    public int playerNum = 0;

    void Start()
    {

        pausemenu.SetActive(false);
        ingameItems.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !mouseLook.waiting)
        {

            if (mouseLook.paused)
            {
                Resume();
            } else
            {
                Pause();
            }

        }

        UpdatePlayerNames();

    }

    void UpdatePlayerNames()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (players.Length == 1)
        {
            player1Name.text = players[0].GetComponent<PlayerMovement>().playerName;
            player2Name.text = "Player not connected";
        } else
        {

            player1Name.text = players[0].GetComponent<PlayerMovement>().playerName;
            player2Name.text = players[1].GetComponent<PlayerMovement>().playerName;

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

        if (playerNum == 1)
        {
            NetworkManager.singleton.StopHost();
            Debug.Log("Server");
        } else
        {
            NetworkManager.singleton.StopClient();
            Debug.Log("Client");
        }  

    }

}
