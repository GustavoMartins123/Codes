using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int num;
    public bool redMshroom= false;
    public bool blueFlower = false;
    public bool purpleMushroom = false;
    public bool brownMushroom = false;
    public bool redFlower = false;
    public bool bread = false;
    public bool bluePotion;

    public bool cheese;

    public bool greenPotion;
    public bool key;
    public bool leafDew;
    public bool meat;

    public bool  pinkEgg;
    public bool purplePotion;
    public bool redPotion;
    public bool roots;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 7)
        {
            AudioManager.instance.Sounds(4);
        }
        if (other.CompareTag("Player"))
        {
            if(redMshroom == true)
            {
                if(Open_Inventory.redMushrooms == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.redMushrooms++;
                Destroy(gameObject);
            }
            else if (blueFlower == true)
            {
                if (Open_Inventory.blueFlowers == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.blueFlowers++;
                Destroy(gameObject);
            }
            else if (purpleMushroom == true)
            {
                if (Open_Inventory.purpleMushrooms == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.purpleMushrooms++;
                Destroy(gameObject);
            }
            else if (brownMushroom == true)
            {
                if (Open_Inventory.brownMushroom == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.brownMushroom++;
                Destroy(gameObject);
            }
            else if(redFlower == true)
            {
                if(Open_Inventory.redFlower == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.redFlower++;
                Destroy(gameObject);
            }
            else if (key== true)
            {
                if (Open_Inventory.key == 0)
                {
                    DysplayIcons();
                }
                Open_Inventory.key++;
                Destroy(gameObject);
            }
            else
            {
                DysplayIcons();
                Destroy(gameObject);
            }

        }
    }

    void DysplayIcons()
    {
        Open_Inventory.newIcon = num;
        Open_Inventory.iconUpdate = true;
    }
}
