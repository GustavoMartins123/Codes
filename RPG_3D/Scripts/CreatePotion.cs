using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePotion : MonoBehaviour
{
    public int[] num;
    public int expectedValue;
    public int value;
    public Image[] emptySlots;
    public Sprite[] icons;
    public Sprite emptyIcon;
    public int itemId = 0;
    int max;
    public int thisValue;
    int max2;
    bool creating = false;
    // Start is called before the first frame update
    void Start()
    {
        expectedValue = num[0];
        max = emptySlots.Length;
        max2 = emptySlots.Length;
    }
    public void Create()
    {
        
        creating = true;
        if (creating)
        {
            for (int i = 0; i < max; i++)
            {
                if (emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[itemId];
                    emptySlots[i].transform.gameObject.GetComponent<Hint_Message>().objectType = itemId + 20;
                    AudioManager.instance.Sounds(3);
                    creating = false;
                    value = 0;
                    thisValue = 0;
                }
            }
            max = emptySlots.Length;
        }
    }
    public void FireStar()
    {
        if(Open_Inventory.pinkEgg > 0 && Open_Inventory.redMushrooms >0 && Open_Inventory.redPotion > 0)
        {
            Create();
            Open_Inventory.pinkEgg--;
            Open_Inventory.redMushrooms--;
            Open_Inventory.redPotion--;
        }
    }
    public void Heal()
    {
        if (Open_Inventory.blueFlowers > 0 && Open_Inventory.greenPotion > 0 && Open_Inventory.leafDew > 0 && Open_Inventory.brownMushroom > 0 && Open_Inventory.roots > 0)
        {
            Create();
            Open_Inventory.blueFlowers--;
            Open_Inventory.greenPotion--;
            Open_Inventory.leafDew--;
            Open_Inventory.brownMushroom--;
            Open_Inventory.roots--;
        }
    }
    public void Invisibility()
    {
        if (Open_Inventory.purplePotion > 0 && Open_Inventory.bluePotion > 0 && Open_Inventory.leafDew > 0 && Open_Inventory.redFlower > 0 && Open_Inventory.purpleMushrooms > 0 && Open_Inventory.redMushrooms > 0)
        {
            Create();
            Open_Inventory.purplePotion--;
            Open_Inventory.bluePotion--;
            Open_Inventory.leafDew--;
            Open_Inventory.redFlower--;
            Open_Inventory.purpleMushrooms--;
            Open_Inventory.redMushrooms--;
        }
    }

    public void Invulnerability()
    {
        if (Open_Inventory.purplePotion > 0 && Open_Inventory.bluePotion > 0 && Open_Inventory.roots > 0 && Open_Inventory.redMushrooms > 0)
        {
            Create();
            Open_Inventory.purplePotion--;
            Open_Inventory.bluePotion--;
            Open_Inventory.roots--;
            Open_Inventory.redMushrooms--;
        }
    }
    public void Strength()
    {
        if (Open_Inventory.pinkEgg > 0 && Open_Inventory.redMushrooms > 0 && Open_Inventory.brownMushroom > 0 && Open_Inventory.roots > 0 && Open_Inventory.purpleMushrooms > 0 && Open_Inventory.greenPotion> 0)
        {
            Create();
            Open_Inventory.pinkEgg --;
            Open_Inventory.redMushrooms--;
            Open_Inventory.brownMushroom--;
            Open_Inventory.roots--;
            Open_Inventory.purpleMushrooms--;
            Open_Inventory.greenPotion--;
        }
    }
    public void Swirl()
    {
        if (Open_Inventory.pinkEgg > 0 && Open_Inventory.purpleMushrooms > 0 && Open_Inventory.purplePotion > 0)
        {
            Create();
            Open_Inventory.pinkEgg--;
            Open_Inventory.purpleMushrooms--;
            Open_Inventory.purplePotion--;
        }
    }
    public void Creating()
    {
        itemId = MagicBook.num;
        if (itemId == 0)
        {
            FireStar();
        }
        else if (itemId == 1)
        {
            Heal();
        }
        else if (itemId == 2)
        {
            Invisibility();
        }
        else if (itemId == 3)
        {
            Invulnerability();
        }
        else if (itemId == 4)
        {
            Strength();
        }
        else if (itemId == 5)
        {
            Swirl();
        }
    }
    public void Remove(int index)
    {
        for (int i = 0; i < max2; i++)
        {
            if(emptySlots[i].sprite == icons[index])
            {
                max2 = i;
                emptySlots[i].sprite = emptyIcon;
                emptySlots[i].transform.gameObject.GetComponent<Hint_Message>().objectType = 0;
            }
        }
        max2 = emptySlots.Length;
    }
    public void UpdateValues()
    {
        value += thisValue;
        expectedValue = num[itemId];
    }
}
