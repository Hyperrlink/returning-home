using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{

    public GameObject hostItems;
    public GameObject clientItems;
    public GameObject mainItems;
    public GameObject optionsItems;
    public AudioSource audioSrc;
    public InputField inputPlayerName;

    public CustomNetworkManager nm;

    public Button startHost;
    public Button startClient;

    public float musicVolume = 1f;
    public int qualityIndex;

    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public string playerName;

    void Start()
    {

        nm = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<CustomNetworkManager>();

        SetupMenuSceneButtons();

        try
        {
            OptionsData op = SaveSystem.LoadOptionsData();

            musicVolume = op.volume;
            qualityIndex = op.qualityIndex;

            volumeSlider.value = musicVolume;
            qualityDropdown.value = qualityIndex;

            NameData nd = SaveSystem.LoadNameData();

            playerName = nd.name;
            inputPlayerName.text = playerName;

        } catch (UnityException e)
        {

        }

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

        playerName = inputPlayerName.text;

    }

    void SetupMenuSceneButtons()
    {

        startHost.onClick.RemoveAllListeners();
        startHost.onClick.AddListener(nm.StartHosting);

        startClient.onClick.RemoveAllListeners();
        startClient.onClick.AddListener(nm.JoinGame);

    }


    // Volume control ----------------------------------------------------------

    public void SetVolume(float vol)
    {

        musicVolume = vol;

    }

    // Quality control ---------------------------------------------------------

    public void SetQuality(int qi)
    {

        qualityIndex = qi;
        QualitySettings.SetQualityLevel(qi);

    }

    // Misc functions ----------------------------------------------------------

    public void Back()
    {

        hostItems.SetActive(false);
        clientItems.SetActive(false);
        optionsItems.SetActive(false);

        mainItems.SetActive(true);

    }

    public void Apply()
    {

        SaveSystem.SaveOptionsData(this);
        SaveSystem.SaveNameData(this);

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
