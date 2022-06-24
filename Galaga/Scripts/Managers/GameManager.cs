using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    static int level = 1, score, maxScore, maxLevel;
    static int lifes = 5;
    int enemyAmount;
    int scoreToBonusLife = 10000;
    static int bonusScore;
    static bool hasLost = false;
    [SerializeField] GameObject clouds, panel;
    bool isPaused;
    private void Awake()
    {
        instance = this;
        /*if (instance == null) 
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }*/
        
        if (hasLost) 
        {
            level = 1;
            score = 0;
            lifes = 5;
            bonusScore = 0;
            
            hasLost = false;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("cloud"))
        {
            MenuManager.clouds = PlayerPrefs.GetInt("cloud");
        }
        else
        {
            MenuManager.clouds = -1;
        }
        maxScore = PlayerPrefs.GetInt("score");
        maxLevel = PlayerPrefs.GetInt("level");
        if(UiScript.instance != null) 
        {
            UiScript.instance.UpdateScore(score);
            UiScript.instance.UpdateLifes(lifes);
            UiScript.instance.UpdateStage(level);
        }
        else
        {

        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if(MenuManager.clouds == -1)
        {
            clouds.SetActive(true);
        }
        else
        {
            clouds.SetActive(false);
        }
    }
    public void AddScore(int amount) 
    {
        score += amount;
        UiScript.instance.UpdateScore(score);
        bonusScore += amount;
        if(bonusScore > scoreToBonusLife) 
        {
            lifes++;
            UiScript.instance.UpdateLifes(lifes);
            bonusScore %= scoreToBonusLife;
        }
    }
    public void AddMaxScore(int amount) 
    {
        if(score > maxScore) 
        {
            maxScore += amount ;
            UiScript.instance.UpdateMaxScore(maxScore);
            PlayerPrefs.SetInt("score", maxScore);
        }
        else
        {
            PlayerPrefs.GetInt("score");
            UiScript.instance.UpdateMaxScore(maxScore);
        }

    }

    public void AddMaxLevel(int amount) 
    {
        if(level > maxLevel) 
        {
            maxLevel += amount;
            UiScript.instance.UpdateMaxLevel(maxLevel);
            PlayerPrefs.SetInt("level", maxLevel);
        }
        else
        {
            PlayerPrefs.GetInt("level");
            UiScript.instance.UpdateMaxLevel(maxLevel);
        }

    }

    public void DecreaseLife() 
    {
        lifes--;
        UiScript.instance.UpdateLifes(lifes);
        if(lifes < 0) 
        {
            Score.score = score;
            Score.level = level;
            Score.maxScore = maxScore;
            Score.maxLevel = maxLevel;
            hasLost = true;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void WinCondition() 
    {
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Panel()
    {
        isPaused = !isPaused;
    }
}
