using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject hostItems;
    public GameObject clientItems;
<<<<<<< HEAD
<<<<<<<< HEAD
    public GameObject mainItems;
    public GameObject optionsItems;
    public AudioSource audioSrc;

    private float musicVolume = 1f;
========
>>>>>>>> parent of 449859a... 0.0.6
=======
>>>>>>> parent of 449859a... 0.0.6

    void Start()
    {

        hostItems.SetActive(false);
        clientItems.SetActive(false);
<<<<<<< HEAD
<<<<<<<< HEAD
        optionsItems.SetActive(false);

        mainItems.SetActive(true);

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
========
>>>>>>>> parent of 449859a... 0.0.6
=======
>>>>>>> parent of 449859a... 0.0.6

    }

    public void StartGame()
    {

<<<<<<< HEAD
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            return;
        }

=======
>>>>>>> parent of 449859a... 0.0.6
        SceneManager.LoadScene("Hub");

    }

<<<<<<< HEAD
<<<<<<<< HEAD
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

========
>>>>>>>> parent of 449859a... 0.0.6
=======
>>>>>>> parent of 449859a... 0.0.6
    public void Exit()
    {

        Application.Quit();

    }

}
