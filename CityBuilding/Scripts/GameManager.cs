using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public int totalWater;
    public int totalMoney;
    public int totalPopulation;
    public int totalFood;
    public int totalWood;
    public float totalFelicity;
    public static GameManager instance 
    {
        get 
        {
            if(_instance == null) 
            {
                GameObject @object = new GameObject("GameManager");
                @object.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
