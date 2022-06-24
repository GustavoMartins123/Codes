using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hint_Message : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject hintBox;
    [SerializeField] Text message;
    Vector3 screenPoint;
    bool displaying = true;
    bool overIcon = false;
    public int objectType = 0;
    [SerializeField] GameObject father;
    [SerializeField] bool magic = false, spells = false;
    Player player;


    private void Start()
    {
        player = PlayerDisplay.instance.players[Save.pchar].GetComponent<Player>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        overIcon = true;
        if (displaying)
        {
            hintBox.SetActive(true);
            if (magic|| magic == false && spells == false)
            {
                screenPoint.x = Input.mousePosition.x + (Screen.height / 2.7f);
            }
            if (spells)
            {
                screenPoint.x = Input.mousePosition.x - (Screen.height / 2.7f);
            }
            screenPoint.y = Input.mousePosition.y;
            screenPoint.z = 1f;
            hintBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            MessageDisplay();
        }

    }



    public void OnPointerExit(PointerEventData eventData)
    {
        overIcon = false;
        hintBox.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (overIcon && Input.GetMouseButtonDown(0))
        {
                AudioManager.instance.Sounds(1);
                displaying = false;
                hintBox.SetActive(false);
                if (magic)
                {
                    if (objectType != 0)
                    {
                        Open_Inventory.instance.selected = objectType - 20;
                        Open_Inventory.instance.set = true;
                    }
                }
                if (spells)
                {
                    if (objectType != 0)
                    {
                        Open_Inventory.instance.selected = objectType - 30;
                        Open_Inventory.instance.set2 = true;
                    }
                }
                if(objectType == 3 && player.healthBar.fillAmount < 1f) 
                {
                    player.Heal(0.2f);
                    Open_Inventory.bread--;
                
                }
                else if(objectType == 5 && player.healthBar.fillAmount < 1f)
            {
                player.Heal(0.3f);
                Open_Inventory.cheese--;
            }
                else if(objectType == 9 && player.healthBar.fillAmount < 1f)
            {
                player.Heal(0.35f);
                Open_Inventory.meat--;
            }
            }
            if (Input.GetMouseButtonUp(0))
            {
                displaying = true;
            }

    }

    private void MessageDisplay()
    {
        if(objectType == 0)
        {
            message.text = "Empty";
        }
        switch (objectType)
        {
            case 1:
                message.text = Open_Inventory.blueFlowers + "x Blue Flower";
                break;
            case 2:
                message.text = Open_Inventory.bluePotion + "x Blue Potion";
                break;
            case 3:
                message.text = Open_Inventory.bread + "x Bread";
                break;
            case 4:
                message.text = Open_Inventory.brownMushroom + "x Brown Mushroom";
                break;
            case 5:
                message.text = Open_Inventory.cheese + "x Cheese";
                break;
            case 6:
                message.text = Open_Inventory.greenPotion + "x Green Potion";
                break;
            case 7:
                message.text = Open_Inventory.key + "x Key";
                break;
            case 8:
                message.text = Open_Inventory.leafDew + "x Leaf Dew";
                break;
            case 9:
                message.text = Open_Inventory.meat + "x Meat";
                break;
            case 10:
                message.text = Open_Inventory.pinkEgg + "x Pink Egg";
                break;
            case 11:
                message.text = Open_Inventory.purpleMushrooms+ "x Purple Mushroom";
                break;
            case 12:
                message.text = Open_Inventory.purplePotion + "x Purple Potion";
                break;
            case 13:
                message.text = Open_Inventory.redFlower + "x Red Flower";
                break;
            case 14:
                message.text = Open_Inventory.redMushrooms+ "x Red Mushroom";
                break;
            case 15:
                message.text = Open_Inventory.redPotion + "x Red Potion";
                break;
            case 16:
                message.text = Open_Inventory.roots + "x Root";
                break;
            case 20:
                message.text = "Explosive Fire Attack";
                break;
            case 21:
                message.text = "Heal 50% of total Health";
                break;
            case 22:
                message.text = "Become invisible for 5 seconds";
                break;
            case 23:
                message.text = "Be invincible for 3 seconds";
                break;
            case 24:
                message.text = "Double the force for 10 seconds";
                break;
            case 25:
                message.text = "Swirl Attack";
                break;
            case 30:
                message.text = "Magic Attack 1";
                break;
            case 31:
                message.text = "Magic Attack 2";
                break;
            case 32:
                message.text = "Magic Attack 3";
                break;
            case 33:
                message.text = "Magic Attack 4";
                break;
            case 34:
                message.text = "Magic Attack 5";
                break;
            case 35:
                message.text = "Magic Attack 6";
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //father.GetComponent<CreatePotion>().thisValue = objectType;
        //father.GetComponent<CreatePotion>().UpdateValues();
    }
}
