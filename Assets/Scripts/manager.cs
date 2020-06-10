using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class manager : MonoBehaviour
{
    bool paused = false;

    public bool accelOrNot = false;

    public Toggle toggle;

    public player player;
    public spawner spawner;

    public GameObject holder;

    public GameObject canvas, scoreCanvas;

    public Image joystick, accel;

    public GameObject perpOrNotOption;

    public TextMeshProUGUI bestScore;

    public float storeTimeScaleOnPause = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                storeTimeScaleOnPause = Time.timeScale;
                Time.timeScale = 0;
                spawner.enabled = false;
                player.enabled = false;
                canvas.SetActive(true);
                scoreCanvas.SetActive(false);
                Camera.main.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnEnable()
    {
        bestScore.text = "Best Score: " +  PlayerPrefs.GetInt("highscore");

        if (PlayerPrefs.GetInt("accel", 0) == 0)
        {
            accelOrNot = false;
        }
        else if (PlayerPrefs.GetInt("accel",0) == 1)
        {
            accelOrNot = true;
        }
        if (PlayerPrefs.GetInt("perp", 1) == 0)
        {
            toggle.isOn = false;
        }
        else if (PlayerPrefs.GetInt("perp",1) == 1)
        {
            toggle.isOn = true;
        }

        AccelOrNot();
        PerpOrNot();
    }

    public void PlayAgain()
    {
        paused = false;
        canvas.SetActive(false);
        Time.timeScale = storeTimeScaleOnPause;
        spawner.enabled = true;
        player.enabled = true;
        scoreCanvas.SetActive(true);
        Camera.main.GetComponent<Rigidbody>().useGravity = true;

        if (!player.accel)
        {
            holder.SetActive(true);
        }
        else
        {
            holder.SetActive(false);
        }
    }    

    public void PerpOrNot()
    {
        player.enabled = true;
        if (toggle.isOn)
        {
            player.perpendicular = true;
            PlayerPrefs.SetInt("perp", 1);
        }
        else
        {
            player.perpendicular = false;
            PlayerPrefs.SetInt("perp", 0);
        }
        player.enabled = false;
    }

    public void AccelOrNot()
    {
        player.enabled = true;

        if (accelOrNot)
        {
            player.accel = true;
            PlayerPrefs.SetInt("accel", 1);
            toggle.gameObject.SetActive(true);
            accel.color = new Color32(255, 255, 255, 137);
            joystick.color = new Color32(255, 255, 255, 0);
            perpOrNotOption.SetActive(true);
        }
        else
        {
            player.accel = false;
            PlayerPrefs.SetInt("accel", 0);
            toggle.gameObject.SetActive(false);
            joystick.color = new Color32(255, 255, 255, 137);
            accel.color = new Color32(255, 255, 255, 0);
            perpOrNotOption.SetActive(false);
        }
        player.enabled = false;
    }

    public void Accelerometer()
    {
        accelOrNot = true;
        AccelOrNot();     
    }

    public void Joystick()
    {
        accelOrNot = false;
        AccelOrNot();  
    }
}
