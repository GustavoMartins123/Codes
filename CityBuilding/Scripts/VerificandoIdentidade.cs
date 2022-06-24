using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Constructs
{
    HouseC = 0,
    LightStand = 1,
    MillBarn = 2,
    Well = 3,
    HouseB = 4
}

public class VerificandoIdentidade : MonoBehaviour
{
    public Constructs constructs;
    Building building;
    float ThouseC = 2f, ThouseB = 2f, TlightStand = 3f, TmillBarn = 5f, Twell = 10f;
    int valores;
    // Start is called before the first frame update
    void Start()
    {
        valores = (int)constructs;
        building = FindObjectOfType<Building>();
        if(valores == 0) 
        {
            building.Population += 3;
            if(building.Felicity <= 100) 
            {
                building.Felicity += 1;
            }
            building.houseC += 1;
        }
        else if(valores == 1) 
        {
            if (building.Felicity <= 100)
            {
                building.Felicity += 2;
            }
            building.lightStand += 1;
        }
        else if(valores == 2) 
        {
            if (building.Felicity <= 100)
            {
                building.Felicity += 1;
            }
            building.Population += 1;
            building.mill += 1;
        }
        else if(valores == 3) 
        {
            if (building.Felicity <= 100)
            {
                building.Felicity += 5;
            }
            building.well += 1;
        }
        else if(valores == 4) 
        {
            building.Population += 2;
            if (building.Felicity < 100) 
            {
                building.Felicity += 0.6f;
            }   
            building.houseB += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(building.Felicity > 0) 
        {
            if (valores == 0)
            {
                ThouseC -= Time.deltaTime;
            }
            else if(valores == 1) 
            {
                TlightStand -= Time.deltaTime;
            }
            else if(valores == 2) 
            {
                TmillBarn -= Time.deltaTime;
            }
            else if(valores == 3) 
            {
                Twell -= Time.deltaTime;
            }
            else if (valores == 4)
            {
                ThouseB -= Time.deltaTime;
            }
            if (ThouseC <= 0 && building.houseC != 0)
            {
                HouseC();
                ThouseC = 2f;
            }
            if(TlightStand <= 0 && building.lightStand != 0) 
            {
                LightStand();
                TlightStand = 3f;
            }
            if(TmillBarn <= 0 && building.mill != 0) 
            {
                MillBarn();
                TmillBarn = 5f;
            }
            if(Twell<= 0 && building.well!=0) 
            {
                Well();
                Twell = 10f;
            }
            if (ThouseB <= 0 && building.houseB != 0)
            {
                HouseB();
                ThouseB = 2f;
            }
        }
        if(building.Felicity > 100) 
        {
            building.Felicity = 100;
        }
    }

    void HouseC() 
    {
        building.Money += 6;
        if(building.Water >= 0) 
        {
            building.Water -= 30;
        }
        if(building.Food > 0) 
        {
            building.Food -= 25;
        }

    }
    void LightStand() 
    {
        building.Felicity += 0.5f;
        building.Wood -= 5;
        building.Money -= 1;
    }
    void MillBarn() 
    {
        building.Food += 200;
        if (building.Water >= 0) 
        {
            building.Water -= 50; 
        }
    }
    void Well() 
    {
        building.Water += 150;
    }
    void HouseB() 
    {
        building.Money += 4;
        if (building.Water >= 2)
        {
            building.Water -= 20;
        }
        if(building.Food >= 0) 
        {
            building.Food -= 16;
        }
    }
}
