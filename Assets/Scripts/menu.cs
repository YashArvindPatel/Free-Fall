using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject infoPanel, exitPanel;
    public GameObject playButton, infoButton;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenInfo()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
        }
        else
        {
            infoPanel.SetActive(true);
        }               
    }

    private void Update()
    {
        if (Time.time > 2)
        {
            playButton.SetActive(true);
            infoButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
    }

    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void YoutubeLink()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=qHcmdM78tCM");
    }
}
