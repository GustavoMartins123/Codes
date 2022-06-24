using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static bool ispaused= false;
    [SerializeField]GameObject panelPause, panelStart, tip;
    //
    [SerializeField] Text exibition, velocity;
    int e = 0;
    int i = 1;
    bool isReady = false;
    void Start()
    {
        if (PlayerPrefs.HasKey("time"))
        {
            i = PlayerPrefs.GetInt("time");
        }
        else 
        {
            i = 1;
        }
        if (PlayerPrefs.HasKey("Exibition")) 
        {
            e = PlayerPrefs.GetInt("Exibition");
        }
        else 
        {
            e = 0;
        }
        if (PlayerPrefs.HasKey("velo")) 
        {
            velocity.text = PlayerPrefs.GetString("velo");
        }
        else 
        {
            velocity.text = "Velocity: 1x";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ispaused = !ispaused;
        }
        if (ispaused)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else if (ispaused == false)
        {
            panelPause.SetActive(false);
            Time.timeScale = i;
        }
        if (Input.GetKeyDown(KeyCode.Numlock)) 
        {
            i++;
            Time01();
            PlayerPrefs.SetInt("time", i);
            PlayerPrefs.SetString("velo", velocity.text);
        }
        SetExibition();
        FullScreen();
    }

    private void Time01()
    {
        switch (i)
        {
            case 1:
                Time.timeScale = 1;
                velocity.text = "Velocity: 1x";
                break;
            case 2:
                Time.timeScale = 2;
                velocity.text = "Velocity: 2x";
                break;
            case 3:
                Time.timeScale = 3;
                velocity.text = "Velocity: 3x";
                break;
            case 4:
                Time.timeScale = 4;
                velocity.text = "Velocity: 4x";
                break;
            default:
                i = 1;
                velocity.text = "Velocity: 1x";
                break;
        }
    }
    public void StartGame() 
    {
        Time.timeScale = i;
        panelStart.SetActive(false);
        ispaused = false;
    }
    public void Quit()
    {
        GameManager.instance.SaveOnBuy();
        PlayerPrefs.SetInt("level", BookMaster.level);
        Application.Quit();
    }
    public void Menu() 
    {
        Time.timeScale = 0;
        panelStart.SetActive(true);
    }
    void SetExibition()
    {
        switch (e)
        {
            case -1:
                e = 1;
                break;
            case 0:
                isReady = false;
                break;
            case 1:
                isReady = true;
                break;
            default:
                e = 0;
                break;
        }
    }
    public void switchExibition()
    {
        e++;
        PlayerPrefs.SetInt("Exibition", e);
    }
    public void switchExibition01()
    {
        e--;
        PlayerPrefs.SetInt("Exibition", e);
    }
    public void FullScreen()
    {
        Screen.fullScreen = isReady;
        if (isReady == true)
        {
            exibition.text = "FullScreen";
        }
        else
        {
            exibition.text = "Windowed";
        }
    }
    public void TipOpen() 
    {
        tip.SetActive(true);
    }
    public void TipClose()
    {
        tip.SetActive(false);
    }
}
