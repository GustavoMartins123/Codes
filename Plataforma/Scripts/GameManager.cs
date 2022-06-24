using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int vida = 3;
    private int vidaInicial = 3;
    public  int pontos;
    private int pontosTotais;
    private int pontosTotais1;
    public int pontosAtuais;
    [SerializeField] private Image[] coracoes;
    [SerializeField] private Text txtPontos;
    [SerializeField] private Text txtPontosAtuais;
    public string cena;

    [SerializeField] private GameObject DangerPanel;
    [SerializeField] private GameObject comoJogar;
    
    /*private void Awake()
    {
        //Garantindo que só existe 1 manager
        //Contando quantos managers existem na cena
        int quantidade = FindObjectsOfType<GameManager>().Length;
        if (quantidade > 3)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }*/
    // Start is called before the first frame update
    void Start()
    {
        AjustaVida();
        Checar();
        pontosAtuais = pontosTotais + pontosTotais1;
        PontosAtuais();
    }

    public void Checar()
    {
        pontosTotais = PlayerPrefs.GetInt("pontos");
        pontosTotais1 = PlayerPrefs.GetInt("pontos1");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Danger() 
    {
        DangerPanel.SetActive(true);
        Destroy(DangerPanel, 3f);
    }
    public void TrocandoDeCena(string cena) 
    {
        SceneManager.LoadScene(cena);
        Time.timeScale = 1f;
    }
    public int GetVida() 
    {
        return vida; 
    }
    public void SetVida(int NovaVida) 
    {
        vida = NovaVida;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("cena1");
        vida = vidaInicial;
        AjustaVida();
        Time.timeScale = 1f;
    }
    public void Ganha() 
    {
        pontos++;
        txtPontos.text = pontos.ToString();
        pontosTotais++;
        PlayerPrefs.SetInt("pontos", pontosTotais);
    }
    public void Ganha1()
    {
        pontos+=20;
        txtPontos.text = pontos.ToString();
        pontosTotais1+=20;
        PlayerPrefs.SetInt("pontos1", pontosTotais1);
    }
    public void AjustaVida() 
    {
        //Rodar pelo meu vetor
        for (var i = 0; i < coracoes.Length; i++) 
        {
            if( i < vida) 
            {
                coracoes[i].enabled = true;
            }
            else 
            {
                coracoes[i].enabled = false;
            }
        }
    }
    public void PontosAtuais()
    {
        txtPontosAtuais.text = pontosAtuais.ToString();
    }
    public void StartJogo() 
    {
        SceneManager.LoadScene("cena1");
        Time.timeScale = 1f;
    }
    public void Quit() 
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void Menu() 
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1f;
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
    public void ComoJogar() 
    {
        comoJogar.SetActive(true);
    }
    public void BackToMenu() 
    {
        comoJogar.SetActive(false);
    }
}
