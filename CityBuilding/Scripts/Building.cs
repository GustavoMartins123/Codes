using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public int Water;
    public int Population;
    public float Felicity;
    public int Wood;
    public int Money;
    public int Food;
    float interval = 0.1f;
    float startTime;
    public int houseC, houseB, well, mill, lightStand;
    float infeliz = 5f, nervosos = 15f, felizes = 10f;
    public bool infelizes = true,feliz = true ;
    private void Awake()
    {
        Felicity = 100;
        Water = 20000;
        Wood = 750;
        Money = 10000;
        Food = 6500;
    }
    private void FixedUpdate()
    {
        startTime += Time.fixedDeltaTime;

        if(startTime > interval) 
        {
            GameManager.instance.totalWater = Water;
            GameManager.instance.totalMoney = Money;
            GameManager.instance.totalPopulation = Population;
            GameManager.instance.totalWood = Wood;
            GameManager.instance.totalFood = Food;
            GameManager.instance.totalFelicity = Felicity;
            startTime = 0;
        }
        if(Water <= 2) 
        {
            infeliz -= Time.fixedDeltaTime;
            nervosos -= Time.fixedDeltaTime;
            Water = 0;
            infelizes = true;
            if(infeliz <= 0 && Felicity >0 && infelizes == true) 
            {
                Felicity -= 4f;
                infeliz = 5f;
            }
            if(nervosos <= 0 && Felicity <= 20) 
            {
                Population -= 2;
                nervosos = 15f;
            }
        }
        else if( Water > 200) 
        {
            infelizes = false;
            felizes -= Time.fixedDeltaTime;
            if(felizes <= 0 && infelizes == false && Felicity <100) 
            {
                Felicity += 5;
            }
        }
        if (Food <= 2)
        {
            infeliz -= Time.fixedDeltaTime;
            nervosos -= Time.fixedDeltaTime;
            Water = 0;
            if (infeliz <= 0 && Felicity > 0)
            {
                Felicity -= 4f;
                infeliz = 5f;
            }
            if (nervosos <= 0 && Felicity <= 20)
            {
                Population -= 2;
                nervosos = 15f;
            }
        }
        else if (Food > 200)
        {
            feliz = false;
            felizes -= Time.fixedDeltaTime;
            if (felizes <= 0 && feliz == false && Felicity < 100)
            {
                Felicity += 5;
            }
        }
    }
}
