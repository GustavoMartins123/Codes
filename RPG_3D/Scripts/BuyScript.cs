using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    [SerializeField] int[] amount;
    [SerializeField] int[] cost;
    [SerializeField] int[] iconsNums;
    [SerializeField] int[] inventoryItems;
    [SerializeField] Text[] itemAmountText;
    [SerializeField] Text currencyText;
    Text compare;
    [SerializeField]bool tavern, wizard;
    int max = 0;
    bool canClick = true;
    // Start is called before the first frame update
    void Start()
    {
        max = itemAmountText.Length;
    }

    public void BuyButton()
    {
        
        if (canClick)
        {
            for (int i = 0; i < max; i++)
            {
                if (itemAmountText[i] == compare)
                {
                    max = i;
                    if (amount[i] > 0)
                    {
                        
                        if (tavern == true)
                        {
                            UpdateTavernAmount();
                        }
                        else if (wizard == true)
                        {
                            UpdateWizardAmount();
                        }
                        if (Open_Inventory.gold >= cost[i])
                        {
                            if (inventoryItems[i] == 0)
                            {
                                Open_Inventory.newIcon = iconsNums[i];
                                Open_Inventory.iconUpdate = true;

                            }
                            Open_Inventory.gold -= cost[i];
                            AudioManager.instance.Sounds(2);
                            if (tavern == true)
                            {
                                SetTavernAmount(i);
                            }
                            else if(wizard == true)
                            {
                                SetWizardAmount(i);
                            }
                        }
                    }
                }
            }
        }
    }

    private void SetTavernAmount(int item)
    {
        if(item == 0)
        {
            Open_Inventory.bread++;
        }
        else if(item == 1)
        {
            Open_Inventory.cheese++;
        }
        else if(item == 2)
        {
            Open_Inventory.meat++;
        }
        amount[item]--;
        itemAmountText[item].text = amount[item].ToString();
        currencyText.text = "x" + Open_Inventory.gold;
        max = itemAmountText.Length;
    }
    private void SetWizardAmount(int item)
    {
        if (item == 0)
        {
            Open_Inventory.redPotion++;
        }
        else if (item == 1)
        {
            Open_Inventory.purplePotion++;
        }
        else if (item == 2)
        {
            Open_Inventory.bluePotion++;
        }
        else if (item == 3)
        {
            Open_Inventory.greenPotion++;
        }
        else if (item == 4)
        {
            Open_Inventory.pinkEgg++;
        }
        else if (item == 5)
        {
            Open_Inventory.roots++;
        }
        else if (item == 6)
        {
            Open_Inventory.leafDew++;
        }
        amount[item]--;
        itemAmountText[item].text = amount[item].ToString();
        currencyText.text = "x" +Open_Inventory.gold;
        max = itemAmountText.Length;
    }
    public void UpdateGold()
    {
        currencyText.text = "x" + Open_Inventory.gold;
    }
    private void UpdateTavernAmount()
    {
        inventoryItems[0] = Open_Inventory.bread;
        inventoryItems[1] = Open_Inventory.cheese;
        inventoryItems[2] = Open_Inventory.meat;
    }
    private void UpdateWizardAmount()
    {
        inventoryItems[0] = Open_Inventory.redPotion;
        inventoryItems[1] = Open_Inventory.purplePotion;
        inventoryItems[2] = Open_Inventory.bluePotion;
        inventoryItems[3] = Open_Inventory.greenPotion;
        inventoryItems[4] = Open_Inventory.pinkEgg;
        inventoryItems[5] = Open_Inventory.roots;
        inventoryItems[6] = Open_Inventory.leafDew;
    }
    public void Bread()
    {
        compare = itemAmountText[0];
        Check(0);
    }
    public void Cheese()
    {
        compare = itemAmountText[1];
        Check(1);
    }
    public void Meat()
    {
        compare = itemAmountText[2];
        Check(2);
    }
    public void RedPotion()
    {
        compare = itemAmountText[0];
        Check(0);
    }
    public void PurplePotion()
    {
        compare = itemAmountText[1];
        Check(1);
    }
    public void BluePotion()
    {
        compare = itemAmountText[2];
        Check(2);
    }
    public void GreenPotion()
    {
        compare = itemAmountText[3];
        Check(3);
    }
    public void PinkEgg()
    {
        compare = itemAmountText[4];
        Check(4);
    }
    public void Roots()
    {
        compare = itemAmountText[5];
        Check(5);
    }
    public void LeafDew()
    {
        compare = itemAmountText[6];
        Check(6);
    }
    void Check(int i)
    {
        if (amount[i] > 0)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }
    }
}
