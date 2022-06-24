using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreensManager : MonoBehaviour
{
    public static ScreensManager instance;
    GameObject currentScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject endScreen;
    public GameObject returnScreen;

    public Button lenghtButton;
    public Button strenghtButton;
    public Button offlineButton;

    public Text gameScreenMoney;
    public Text lenghtCostText;
    public Text lenghtValueText;
    public Text strenghtCostText;
    public Text strenghtValueText;
    public Text offlineCostText;
    public Text offlineValueText;
    public Text endScreenMoney;
    public Text returnScreenMoney;

    int gameCount;
    // Start is called before the first frame update
    void Awake()
    {
        if (ScreensManager.instance) 
        {
            Destroy(base.gameObject);
        }
        else 
        {
            instance = this;
        }
        currentScreen = mainScreen;
    }
    private void Start()
    {
        CheckIdles();
        UpdateTexts();
    }
    private void Update()
    {
        CheckIdles();
        UpdateTexts();
    }
    public void ChangeScreen(Screen screen) 
    {
        currentScreen.SetActive(false);
        switch (screen) 
        {
            case Screen.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;
            case Screen.GAME:
                currentScreen = gameScreen;
                gameCount++;
                break;
            case Screen.END:
                currentScreen = endScreen;
                SetEndScreenMoney();
                break;
            case Screen.RETURN:
                currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;
        }
        currentScreen.SetActive(true);
    }

    private void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + IdleManager.instance.totalGain + " Gained offline!";
    }

    private void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain ;
    }

    public void UpdateTexts() 
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lenghtCostText.text = "$" + IdleManager.instance.lenghtCost;
        lenghtValueText.text = -IdleManager.instance.length + "m";
        strenghtCostText.text = "$" + IdleManager.instance.strenghtCost;
        strenghtValueText.text = IdleManager.instance.strenght + " Fishes.";
        offlineCostText.text = "$"+ IdleManager.instance.offlineEarningsCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + "/Min";
    }
    public void CheckIdles() 
    {
        int lenghtCost = IdleManager.instance.lenghtCost;
        int strenghtCost = IdleManager.instance.strenghtCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if(wallet < lenghtCost) 
        {
            lenghtButton.interactable = false;
        }
        else 
        {
            lenghtButton.interactable = true;
        }
        if (wallet < strenghtCost)
        {
            strenghtButton.interactable = false;
        }
        else
        {
            strenghtButton.interactable = true;
        }
        if (wallet < offlineEarningsCost)
        {
            offlineButton.interactable = false;
        }
        else
        {
            offlineButton.interactable = true;
        }
    }
}
