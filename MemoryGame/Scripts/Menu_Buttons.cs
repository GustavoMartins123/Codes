using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    int d = 0;
    [SerializeField] GameObject panel_Difficulty, menu;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            panel_Difficulty = null;
        }
        d = PlayerPrefs.GetInt("difi", 1);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(d);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ApplicationQuitGame()
    {
        Application.Quit();
    }
    public void Difficulty()
    {
        panel_Difficulty.SetActive(true);
        menu.SetActive(false);
    }
    public void Difficulty_Selected(int i)
    {
        d = i;
        PlayerPrefs.SetInt("difi", d);
        menu.SetActive(true);
        panel_Difficulty.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
