using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionIcons : MonoBehaviour
{
    public GameObject father;
    public int objId;
    public Image thisImage;
    public Color32 startColor = new Color32(255,255,255,120);
    public Color32 endColor = new Color32(255, 255, 255, 255);
    [SerializeField] GameObject inventory;
    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            switch (objId)
            {
                case 1:
                    if (Open_Inventory.blueFlowers > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 2:
                    if (Open_Inventory.bluePotion > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 4:
                    if (Open_Inventory.brownMushroom > 0)
                    {
                        thisImage.color = endColor;
                        
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 6:
                    if (Open_Inventory.greenPotion > 0)
                    {
                        thisImage.color = endColor;
                        
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 8:
                    if (Open_Inventory.leafDew > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 10:
                    if (Open_Inventory.pinkEgg > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 11:
                    if (Open_Inventory.purplePotion > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 12:
                    if (Open_Inventory.purpleMushrooms > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 13:
                    if (Open_Inventory.redFlower > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 14:
                    if (Open_Inventory.redMushrooms > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 15:
                    if (Open_Inventory.redPotion > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;
                case 16:
                    if (Open_Inventory.roots > 0)
                    {
                        thisImage.color = endColor;
                    }
                    else
                    {
                        thisImage.color = startColor;
                        Open_Inventory.instance.RemoveIcon(objId);
                    }
                    break;

            }
            if (check == true)
            {
                check = false;
                Open_Inventory.instance.CheckStats();
            }

        }
    }
}
