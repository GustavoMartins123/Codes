using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text highScore, level, maxScore, maxLevel;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "Score: " + Score.score;
        level.text = "Level Reached: " + Score.level;
        if (Score.score > Score.maxScore)
        {
            maxScore.text = "High Score: " + Score.score;
        }
        else
        {
            maxScore.text = "High Score: " + Score.maxScore;
        }
        if (Score.level > Score.maxLevel)
        {
            maxLevel.text = "Highest Level: "+Score.level;
        }
        else
        {
            maxLevel.text = "Highest Level: " + Score.maxLevel;
        }

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
