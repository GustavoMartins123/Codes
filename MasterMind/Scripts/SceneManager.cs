using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{


    /// Menu
    [SerializeField] GameObject panel;
    bool panelInformation;
    //


    //It's like this because I was too lazy to create another script, I ended up not even looking at the name I put :<


    //
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex -1);
    }
    public void HowToPlay()
    {
        panelInformation = !panelInformation;
        panel.SetActive(panelInformation);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
