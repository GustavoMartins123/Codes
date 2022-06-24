using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private void Awake()
    {
        //Garantindo que só existe 1 manager
        //Contando quantos managers existem na cena
        int quantidade = FindObjectsOfType<Manager>().Length;
        if(quantidade > 1) 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] private Text textoPontos;
    private bool IsPaused;

    [Header("Paineis e menu")]
    public GameObject PausePanel;
    public string cena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    public void Jogar() 
    {
        SceneManager.LoadScene("Jogo");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        // Jogo Compilado
        Application.Quit();
    }
    public void back()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    void PauseScreen()
    {
        if (IsPaused)
        {
            IsPaused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            IsPaused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    //Criando um metodo que so roda depois de um tempo
    IEnumerator GameOverLay()
    {
        yield return new WaitForSeconds(2f);
        //Todo codigo daqui so vai rodar depois de 2 segundos
        SceneManager.LoadScene("GameOver");
    }
    public void GameOverlay2() 
    {
        StartCoroutine(GameOverLay());
    }
}
