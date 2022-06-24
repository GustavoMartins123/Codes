using UnityEngine;
using UnityEngine.UI;

public class BuyWeapons : MonoBehaviour
{
    [SerializeField] int weapon;
    [SerializeField] int cost;
    [SerializeField] Text currencyText;
    [SerializeField] Open_Inventory inventoryObj;
    public void BuyWeapon()
    {
        if(Open_Inventory.gold > cost&& inventoryObj.weapons[weapon] == false)
        {
            Open_Inventory.gold -= cost;
            currencyText.text = "x" + Open_Inventory.gold;
            inventoryObj.weapons[weapon] = true;
            AudioManager.instance.Sounds(2);
            if(Open_Inventory.instance.objectives[5] == false)
            {
                Open_Inventory.instance.UpdateMessage(true, 5, 100);
            }
        }
    }
}
