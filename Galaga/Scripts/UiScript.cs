using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public static UiScript instance;
    public Text score, lifes, stage, maxScore, maxLevel;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    public void UpdateScore(int amount)
    {
        score.text = amount.ToString("D9");
    }
    public void UpdateStage(int amount) 
    {
        stage.gameObject.SetActive(true);
        stage.text = "Stage " + amount;

        Invoke("DesactiveStage", 3.3f);
    }

    public void UpdateLifes(int amount) 
    {

        lifes.text ="x" +amount.ToString("D2");
    }

    public void UpdateMaxScore(int amount)
    {

        maxScore.text = "High Score: " + amount.ToString();
    }
    public void UpdateMaxLevel(int amount)
    {

        maxLevel.text = "High Level: " + amount.ToString();
    }
    void DesactiveStage()
    {
        stage.gameObject.SetActive(false);
        CancelInvoke("DesactiveStage");
    }
}
