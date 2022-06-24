using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleManager : MonoBehaviour
{
    [HideInInspector]
    public int length;
    [HideInInspector]
    public int strenght;
    [HideInInspector]
    public int offlineEarnings;
    [HideInInspector]
    public int lenghtCost;
    [HideInInspector]
    public int strenghtCost;
    [HideInInspector]
    public int offlineEarningsCost;
    [HideInInspector]
    public int wallet;
    [HideInInspector]
    public int totalGain;
    int lenghtCount, strenghtCount, offlineCount;
    float time = 1f;
    int[] costs = new int[] 
    {
        140,
        210,
        340,
        500,
        820,
        1020,
        1550,
        2000,
        2950,
        4100,
        6325,
        9300,
        12340,
        17895,
        24900,
        33470,
        46935,
        54330,
        71660,
        154370
    };
    public static IdleManager instance;

    private void Awake()
    {
        if (instance) 
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
        lenghtCost = costs[lenghtCount];
        strenghtCost = costs[strenghtCount];
        offlineEarningsCost = costs[offlineCount];
    }
    private void Start()
    {
        Debug.Log(costs[1]);
        if (PlayerPrefs.HasKey("wallet"))
        {
            wallet = PlayerPrefs.GetInt("wallet");
        }
        else
        {
            wallet = 0;
        }
        if (PlayerPrefs.HasKey("offline"))
        {
            offlineEarnings = PlayerPrefs.GetInt("offline");
        }
        else
        {
            offlineEarnings = 3;
        }
        if (PlayerPrefs.HasKey("strenght"))
        {
            strenght = PlayerPrefs.GetInt("strenght");
        }
        else
        {
            strenght = 3;
        }
        if (PlayerPrefs.HasKey("lenght"))
        {
            length = -PlayerPrefs.GetInt("lenght");
        }
        else
        {
            length = -30;
        }
        if (PlayerPrefs.HasKey("countLen")) 
        {
            lenghtCount = PlayerPrefs.GetInt("countLen");
        }
        else 
        {
            lenghtCount = 0;
        }
        if (PlayerPrefs.HasKey("countStr")) 
        {
            strenghtCount = PlayerPrefs.GetInt("countStr");
        }
        else 
        {
            strenghtCount = 0;
        }
        if (PlayerPrefs.HasKey("countOff")) 
        {
            offlineCount = PlayerPrefs.GetInt("countOff");
        }
        else 
        {
            offlineCount = 0;
        }
        if (lenghtCount == 19)
        {
            ScreensManager.instance.lenghtButton.interactable = false;
        }
        if (strenghtCount == 19)
        {
            ScreensManager.instance.strenghtButton.interactable = false;
        }
        if (offlineCount == 19)
        {
            ScreensManager.instance.strenghtButton.interactable = false;
        }
        lenghtCost = costs[lenghtCount];
        strenghtCost = costs[strenghtCount];
        offlineEarningsCost = costs[offlineCount];
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) 
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("date", now.ToString());
            if (Hook.instance.CanMove) 
            {
                Hook.instance.StopFishing();
            }

        }
        else 
        {
            string strin = PlayerPrefs.GetString("date");
            if(strin != string.Empty) 
            {
                offlineEarnings = PlayerPrefs.GetInt("offline");
                DateTime d = DateTime.Parse(strin);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings);
                ScreensManager.instance.ChangeScreen(Screen.RETURN);
            }
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("date", now.ToString());
        }
        else
        {
            string strin = PlayerPrefs.GetString("date");
            offlineEarnings = PlayerPrefs.GetInt("offline");
            if (strin != string.Empty)
            {
                DateTime d = DateTime.Parse(strin);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings); //* offlineEarnings);
                ScreensManager.instance.ChangeScreen(Screen.RETURN);
            }
        }
    }
    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
        OnApplicationFocus(false);

    }

    public void BuyLenght() 
    {
        if(lenghtCount < 20) 
        {
            lenghtCount++;


            length -= 25;
            wallet -= lenghtCost;
            PlayerPrefs.SetInt("lenght", -length);
            PlayerPrefs.SetInt("wallet", wallet);
            PlayerPrefs.SetInt("countLen", lenghtCount);
            ScreensManager.instance.ChangeScreen(Screen.MAIN);

        }

    }
    public void BuyStrenght()
    {
        if(strenghtCount < 20)
        {
            strenghtCount++;


            strenght++;
            wallet -= strenghtCost;
            PlayerPrefs.SetInt("strenght", strenght);
            PlayerPrefs.SetInt("wallet", wallet);
            PlayerPrefs.SetInt("countStr", strenghtCount);
            ScreensManager.instance.ChangeScreen(Screen.MAIN);
        }

    }

    public void BuyOfflineEarnings()
    {
        if(offlineCount < 20) 
        {
            offlineCount++;


            offlineEarnings += 2 * offlineCount;
            wallet -= offlineEarningsCost;
            PlayerPrefs.SetInt("offline", offlineEarnings);
            PlayerPrefs.SetInt("wallet", wallet);
            PlayerPrefs.SetInt("countOff", offlineCount);
            ScreensManager.instance.ChangeScreen(Screen.MAIN);
        }


    }

    void VeriPrice() 
    {
        lenghtCost = costs[lenghtCount];
        offlineEarningsCost = costs[offlineCount];
        strenghtCost = costs[strenghtCount];
    }
    public void ColletMoney() 
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screen.MAIN);
    }
    private void Update()
    {
        if(lenghtCount == 19) 
        {
            ScreensManager.instance.lenghtButton.interactable = false;
        }
        if(strenghtCount == 19) 
        {
            ScreensManager.instance.strenghtButton.interactable = false;
        }
        if(offlineCount == 19) 
        {
            ScreensManager.instance.offlineButton.interactable = false;
        }
        VeriPrice();
        time -= Time.deltaTime;
        if(time <= 0) 
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("date", now.ToString());
            time = 1f;
        }
    }
    public void Sair() 
    {
        Application.Quit();
    }
}
