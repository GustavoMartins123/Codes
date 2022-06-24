using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRecursos : MonoBehaviour
{
    [SerializeField] Text wood, water, money, food, population, felicity;
    public static bool abrir, acelerar;
    int ac = 0;
    [SerializeField] GameObject buildingUI;
    float tempo = 1f;
    // Start is called before the first frame update
    void Start()
    {
        wood.text = "Wood: " + GameManager.instance.totalWood.ToString();
        water.text = "Water: " + GameManager.instance.totalWater.ToString();
        money.text = "Money: " + GameManager.instance.totalMoney.ToString();
        food.text = "Food: " + GameManager.instance.totalFood.ToString();
        population.text = "Population: " + GameManager.instance.totalPopulation.ToString();
        felicity.text = "Felicity: " + GameManager.instance.totalFelicity.ToString();
    }

    void verificandoRecursos() 
    {
        wood.text = "Wood: " +GameManager.instance.totalWood.ToString();
        water.text = "Water: " +GameManager.instance.totalWater.ToString();
        money.text = "Money: "+GameManager.instance.totalMoney.ToString();
        food.text = "Food: " +GameManager.instance.totalFood.ToString();
        population.text = "Population: "+GameManager.instance.totalPopulation.ToString();
        felicity.text = "Felicity: " + GameManager.instance.totalFelicity.ToString();
    }
    private void Update()
    {
        AbrindoBuilding();
        if (Input.GetKeyDown(KeyCode.Numlock)) 
        {
            ac++;
        }
        switch (ac) 
        {
            case 0:
                Time.timeScale = 1;
                break;
            case 1:
                Time.timeScale = 2;
                break;
            case 2:
                Time.timeScale = 3;
                break;
            default:
                ac = 0;
                break;
        }
    }

    private void AbrindoBuilding()
    {
        tempo -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.I))
        {
            abrir = !abrir;
        }
        if (abrir == true)
        {
            buildingUI.SetActive(true);
        }
        else
        {
            buildingUI.SetActive(false);
        }
        if (tempo <= 0)
        {
            verificandoRecursos();
            tempo = 1f;
        }
    }
}
