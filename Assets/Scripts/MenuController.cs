using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject hostItems;
    public GameObject clientItems;

    void Start()
    {

        hostItems.SetActive(false);
        clientItems.SetActive(false);

    }

    public void StartGame()
    {

        SceneManager.LoadScene("Hub");

    }

    public void Exit()
    {

        Application.Quit();

    }

}
