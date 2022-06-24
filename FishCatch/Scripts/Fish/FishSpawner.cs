using UnityEngine;
using System;

public class FishSpawner : MonoBehaviour
{
    private void Awake()
    {
        for(int i = 0; i < fishTypes.Length; i++) 
        {
            int num = 0;
            while(num < fishTypes[i].fishCount) 
            {
                Fish fish = Instantiate<Fish>(fishPre);
                fish.Type = fishTypes[i];
                fish.ResetFish();
                num++;
            }
        }
    }
    [SerializeField] Fish fishPre;
    [SerializeField] Fish.FishType[] fishTypes;
}
