using UnityEngine;

[CreateAssetMenu(fileName = "Icons", menuName = "Idle Game")]
public class Icons : ScriptableObject
{
    public Sprite sprite;
    public Sprite unknowIcon;
    public string Name;
    public float basePrice;
    public float multiplier;
    public float baseIncome;

    public float CalculateCost(int amount) 
    {
        float newPrice= basePrice * Mathf.Pow(multiplier, amount);
        float round = (float)Mathf.Round(newPrice*100)/100;
        return round;
    }

    public float CalculateIncome(int amount) 
    {
        return baseIncome * amount;
    }
}
