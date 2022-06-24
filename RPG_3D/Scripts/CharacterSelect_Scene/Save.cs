using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static int pchar = 0;
    public static string pname = "Player";
    public static float manaAmount = 1f;
    public static float staminaAmount = 1f;
    public static float healthBar;
    public static float costSpell;
    public static bool invisible = false;
    public static bool invincibility = false;
    public static bool Strength = false;
    public static float strenghtPower = 0.1f;
    public static float manaPower = 0.1f;
    public static float staminaPower = 0.1f;
    public static int weaponChoice = 0;
    public static bool weaponChange = false;
    public static bool carryingWeapon = false;
    public static float playerLevel =0.1f;
    public static float xpLvl = 0;
    public bool levelingUP = false;
    bool IamStronger= false;
    public static float xpToLvl = 75;
    bool full = false;
    float sobra;
    WaitForSeconds wait = new WaitForSeconds(10f);
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(manaAmount< 1f)
        {
            manaAmount += 0.025f * Time.deltaTime;
        }
        else if (manaAmount <= 0)
        {
            manaAmount = 0;
        }
        if(staminaAmount < 1f)
        {
            staminaAmount += 0.055f * Time.deltaTime;
        }
        else if(staminaAmount <= 0)
        {
            staminaAmount = 0;
        }
        if (Strength && IamStronger == false)
        {
            IamStronger = true;
            StartCoroutine(Weak());
        }
        if(xpLvl > xpToLvl && full == false && Strength == false)
        {
            sobra = xpLvl - xpToLvl;
            if (manaPower == 1 && strenghtPower == 1 && staminaPower == 1)
            {
                Stop();
            }
            levelingUP = true;
            if (levelingUP)
            {
                levelingUP = false;
                xpToLvl += (xpToLvl*3)/2;
                xpLvl = 0;
                if (sobra > 0)
                {
                    xpLvl += sobra;
                    sobra = 0;
                }
                if (playerLevel < 1.3f)
                {
                    playerLevel += 0.1f;
                }
                int random = Random.Range(0, 3);
                    if (random == 0 && strenghtPower < 1f)
                    {
                        strenghtPower += 0.1f;
                    }
                    else if (random == 1 && manaPower < 1)
                    {
                        manaPower += 0.1f;
                    }
                    else if (random == 2 && staminaPower < 1)
                    {
                        staminaPower += 0.1f;
                    }
                    else if (staminaPower == 1)
                    {
                        random = Random.Range(0, 2);
                        if (random == 0 && manaPower < 1f)
                        {
                            manaPower += 0.1f;
                        }
                        else if (random == 1 && strenghtPower < 1)
                        {
                            strenghtPower += 0.1f;
                        }
                    }
                    else if (strenghtPower == 1)
                    {
                        random = Random.Range(0, 2);
                        if (random == 0 && manaPower < 1f)
                        {
                            manaPower += 0.1f;
                        }
                        else if (random == 1 && staminaPower < 1)
                        {
                            staminaPower += 0.1f;
                        }
                    }
                    else if (manaPower == 1)
                    {
                        random = Random.Range(0, 2);
                        if (random == 0 && staminaPower < 1f)
                        {
                            manaPower += 0.1f;
                        }
                        else if (random == 1 && strenghtPower < 1)
                        {
                            strenghtPower += 0.1f;
                        }
                    }
            }
        }
    }
    void Stop()
    {
        full = true;
    }

    void PowerUp()
    {
        strenghtPower = strenghtPower * 2;
        manaPower = manaPower * 2;
        staminaPower = staminaPower *2;
    }
    void PowerDown()
    {
        IamStronger = false;
        strenghtPower = strenghtPower / 2;
        manaPower = manaPower / 2;
        staminaPower = staminaPower / 2;
    }
    IEnumerator Weak()
    {
        PowerUp();
        yield return wait;
        PowerDown();
    }
}
