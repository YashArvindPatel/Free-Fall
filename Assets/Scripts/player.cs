using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class player : MonoBehaviour {

    Rigidbody rigid;

    public float speed = 10f;

    public bool perpendicular = true;

    public bool accel = false;

    public FloatingJoystick floatingJoystickL;
    public FloatingJoystick floatingJoystickR;

    public GameObject back1, back2;

    public GameObject border;

    public float score = 0;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {       
        if (PlayerPrefs.GetInt("perp") == 1)
        {
            perpendicular = true;
        }
        else if (PlayerPrefs.GetInt("perp" , 1) == 0)
        {
            perpendicular = false;
        }

        if (PlayerPrefs.GetInt("accel") == 1)
        {
            accel = true;
            speed = 20f;
        }
        else if (PlayerPrefs.GetInt("accel", 0) == 0)
        {
            accel = false;
            speed = 10f;
        }
    }

    void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	void Update () {

        score += Time.deltaTime;
        if ((int)score < 10)
        {
            scoreText.text = "0" + (int)score;
        }
        else
        {
            scoreText.text = ((int)score).ToString();
        }        
        
        transform.rotation *= Quaternion.Euler(0, 0, 7 * Time.deltaTime);
        Time.timeScale += Time.fixedDeltaTime * 0.01f;

        if (accel)
        {
            if (perpendicular)
            {
                rigid.velocity += transform.rotation * (Vector3.right * Input.acceleration.x * speed * Time.deltaTime);
                rigid.velocity += transform.rotation * (Vector3.up * -Input.acceleration.z * speed * Time.deltaTime);
            }
            else
            {
                rigid.velocity += transform.rotation * (Vector3.right * Input.acceleration.x * speed * Time.deltaTime);
                rigid.velocity += transform.rotation * (Vector3.up * Input.acceleration.y * speed * Time.deltaTime);
            }
        }
        else
        {
            if (back1.activeSelf)
            {
                rigid.velocity += transform.rotation * (Vector3.right * floatingJoystickL.Horizontal * speed * Time.deltaTime);
                rigid.velocity += transform.rotation * (Vector3.up * floatingJoystickL.Vertical * speed * Time.deltaTime);
            }
            else if (back2.activeSelf)
            {
                rigid.velocity += transform.rotation * (Vector3.right * floatingJoystickR.Horizontal * speed * Time.deltaTime);
                rigid.velocity += transform.rotation * (Vector3.up * floatingJoystickR.Vertical * speed * Time.deltaTime);
            }
        }

        border.transform.position = new Vector3(border.transform.position.x, Camera.main.transform.position.y, border.transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("highscore", (int)score);
    }
}
