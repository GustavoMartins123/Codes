using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject[] fundo;
    [SerializeField] Text backText, cloudsText;
    int backGround = -1;
    public static int clouds = -1;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("back"))
        {
            backGround = PlayerPrefs.GetInt("back");
        }
        else
        {
            backGround = -1;
        }
        if (PlayerPrefs.HasKey("cloud"))
        {
            clouds = PlayerPrefs.GetInt("cloud");
        }
        else
        {
            clouds = -1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        fund();
        Clouds();
    }
    public void StartMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsOpen()
    {
        panel.SetActive(true);
    }
    public void OptionsClose()
    {
        panel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void fund()
    {
        switch (backGround)
        {
            case -1:
                fundo[backGround+1].SetActive(true);
                fundo[backGround + 2].SetActive(false);
                backText.text = "BackGround01";
                PlayerPrefs.SetInt("back", backGround);
                break;
            case 1:
                fundo[backGround].SetActive(true);
                fundo[backGround - 1].SetActive(false);
                backText.text = "BackGround02";
                PlayerPrefs.SetInt("back", backGround);
                break;
            default:
                backGround = -1;
                break;
        }
    }
    void Clouds()
    {
        switch (clouds)
        {
            case -1:
                cloudsText.text = "On";
                PlayerPrefs.SetInt("cloud", clouds);
                break;
            case 1:
                cloudsText.text = "Off";
                PlayerPrefs.SetInt("cloud", clouds);
                break;
        }
    }
    public void back()
    {
        backGround *= -1;
    }
    public void Clo()
    {
        clouds *= -1;
    }
}
