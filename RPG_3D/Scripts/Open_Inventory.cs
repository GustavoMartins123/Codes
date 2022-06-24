using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Open_Inventory : MonoBehaviour
{
    public static Open_Inventory instance;
    [SerializeField] GameObject[] objects;
    public static bool active;
    [SerializeField] Image[] emptySlots;
    [SerializeField]Sprite[] icons;
    [SerializeField] Sprite empty_Icon;

    public static int redMushrooms = 0;
    public static int blueFlowers = 0;
    public static int purpleMushrooms = 0;
    public static int brownMushroom = 0;
    public static int redFlower = 0;

    public static int bread = 0;
    public static int bluePotion = 0;
    public static int cheese = 0;
    public static int greenPotion=0;
    public static int key = 0;
    public static int leafDew = 0;
    public static int meat = 0;
    public static int pinkEgg=0;
    public static int purplePotion = 0;
    public static int redPotion = 0;
    public static int roots = 0;

    public static int newIcon=0;
    public static int gold = 0;
    public static bool iconUpdate = false;
    int max;

    public string entry;
    public string[] items;
    public int currentId = 0;
    public int checkAmount = 0;
    int max2;
    int max3;

    [SerializeField] Image[] UiSlots;
    [SerializeField] Sprite[] magicIcons, spellsIcons;
    [SerializeField] KeyCode[] keys;
    public bool set = false, set2 = false;
    public int selected = 0;
    [SerializeField] int[] magicAttack;
    [SerializeField] GameObject potion;
    [SerializeField] GameObject[] particles;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject player;
    [SerializeField] Image manaBar, staminaBar, expBar;
    [SerializeField] Text[] currencysStats;
    [HideInInspector]public bool magic = false;
    [SerializeField] Image[] statsPower;
    public bool[] weapons;
    [SerializeField] GameObject[] wepons;
    WaitForSeconds wait = new WaitForSeconds(0.15f);
    WaitForSeconds wait2 = new WaitForSeconds(0.33f);
    WaitForSeconds wait3 = new WaitForSeconds(1f);
    public Text[] messages;
    public bool[] objectives;
    public int maxFour;
    public Button[] objectivesReceived;
    public bool spider, dragon;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        max = emptySlots.Length;
        max2 = items.Length;
        max3 = emptySlots.Length;
        maxFour = messages.Length;
        redMushrooms = 0;
        blueFlowers = 0;
        purpleMushrooms = 0;
        brownMushroom = 0;
        redFlower = 0;
        bread = 0;
        bluePotion = 0;
        cheese = 0;
        greenPotion = 0;
        key = 0;
        leafDew = 0;
        meat = 0;
        pinkEgg = 0;
        purplePotion = 0;
        redPotion = 0;
        roots = 0;
        currencysStats[0].text = Save.pname;
        currencysStats[1].text = "x" +gold;
        StartCoroutine(handPl());
        /*if(Save.pchar >=0 && Save.pchar< 3)
        {
            wepons[6].SetActive(true);
            wepons[7].SetActive(false);
        }
        else if(Save.pchar >= 3)
        {
            wepons[6].SetActive(false);
            wepons[7].SetActive(true);
        }*/
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Player.interaction == false)
        {
            active = !active;
            if(key<=0 && icons[7] != emptySlots[0])
            {
                RemoveIcon(7);
            }
            if (PlayerDisplay.instance.characters[Save.pchar].activeSelf == false)
            {
                PlayerDisplay.instance.characters[Save.pchar].SetActive(true);
            }
            
            for (int i = 0; i < 3; i++)
            {
                objects[i].SetActive(active);
            }
            currencysStats[1].text = "x" + gold;
            objects[3].SetActive(!active);
            AudioManager.instance.Sounds(0);
            if (active)
            {
                Time.timeScale = 0;
                if (objects[4].activeSelf|| objects[7].activeSelf)
                {
                    objects[6].SetActive(false);
                }
                else
                {
                    statsPower[0].fillAmount = Save.strenghtPower;
                    statsPower[1].fillAmount = Save.manaPower;
                    statsPower[2].fillAmount = Save.staminaPower;
                    objects[6].SetActive(true);
                }
            }
            else
            {
                objects[6].SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (bread <= 0 && icons[3] != emptySlots[0])
        {
            RemoveIcon(3);
        }
        else if (cheese <= 0 && icons[5] != emptySlots[0])
        {
            RemoveIcon(5);
        }
        else if (meat <= 0 && icons[9] != emptySlots[0])
        {
            RemoveIcon(9);
        }
        if (iconUpdate == true)
        {
            for (int i = 0; i < max; i++)
            {
                if (emptySlots[i].sprite == empty_Icon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[newIcon];
                    emptySlots[i].transform.gameObject.GetComponent<Hint_Message>().objectType = newIcon;
                }
            }
            StartCoroutine(ResetNum());
        }
        for (int i = 0; i < wepons.Length; i++)
        {
            if (weapons[i] == true)
            {
                wepons[i].SetActive(true);
            }
        }

        if (set)
        {
            for (int i = 0; i < UiSlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    set = false;
                    UiSlots[i].sprite = magicIcons[selected];
                    magicAttack[i] = selected;
                    potion.GetComponent<CreatePotion>().Remove(selected);
                }
            }
        }
        if (set2)
        {
            for (int i = 0; i < UiSlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    set2 = false;
                    UiSlots[i].sprite = spellsIcons[selected];
                    magicAttack[i] = selected + 6;
                }
            }
        }
        Magic();
        staminaBar.fillAmount = Save.staminaAmount;
        manaBar.fillAmount = Save.manaAmount;
        expBar.fillAmount = Save.xpLvl / Save.xpToLvl;
    }

    public void UpdateMessage(bool complete, int i, int amount)
    {
            objectives[i] = complete;
        if (objectives[i])
        {
            if (messages[i].text == "Blank")
            {
                maxFour = i;
                messages[i].color = Color.clear;
            }
            else if (messages[i].text != "Blank")
            {
                messages[i].color = Color.white;
            }
            messages[i].color = Color.green;
            
            AudioManager.instance.Sounds(2);
        }
        gold += amount;
        maxFour = messages.Length;
    }
    private void Magic()
    {
        if (Input.anyKey && Time.timeScale == 1)
        {
            for (int i = 0; i < UiSlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    if (UiSlots[i].sprite != empty_Icon)
                    {
                        if(Save.manaAmount > Save.costSpell && magic == false)
                        {
                            magic = true;
                            manaBar.fillAmount = Save.manaAmount;
                            Player.anim.SetTrigger("spell");
                            StartCoroutine(MagicAttack(i));
                            AudioManager.instance.Sounds(magicAttack[i] + 5);
                            StartCoroutine(MagicReset());
                        }
                        if(magicAttack[i] < 6 && Save.manaAmount > Save.costSpell)
                        {
                            UiSlots[i].sprite = empty_Icon;
                        }
                    }
                }
            }
        }
    }

    IEnumerator ResetNum()
    {
        yield return wait;
        iconUpdate = false;
        max = emptySlots.Length;
    }
    IEnumerator MagicAttack(int i)
    {
        yield return wait2;
        if(magicAttack[i]!= 2)
        {
            Instantiate(particles[magicAttack[i]], hand.transform.position, player.transform.rotation);
        }
        else
        {
            Instantiate(particles[magicAttack[i]], hand.transform.position, particles[magicAttack[i]].transform.rotation);
        }
        
    }
    IEnumerator handPl()
    {
        yield return wait;
        hand = GameObject.FindGameObjectWithTag("Hand");
        player = PlayerDisplay.instance.players[Save.pchar].gameObject;
        //player = GameObject.FindGameObjectWithTag("Player");
    }
    public void CheckStats()
    {
        for (int i = 0; i < max2; i++)
        {
                max2 = i;
                entry = items[i];
                checkAmount = Convert.ToInt32(typeof(Open_Inventory).GetField(entry).GetValue(null));
                checkAmount--;
                typeof(Open_Inventory).GetField(entry).SetValue(null, checkAmount);
                if(checkAmount == 0)
                {
                    RemoveIcon(i);
                }
        }
        max2 = items.Length;
    }
    IEnumerator MagicReset()
    {
        yield return wait3;
        magic = false;
    }
    public void Stats()
    {
        for (int i = 5; i < 7; i++)
        {
            objects[i].SetActive(true);
        }
        objects[4].SetActive(false);
        if (objects[7].activeSelf)
        {
            objects[7].SetActive(false);
        }
        statsPower[0].fillAmount = Save.strenghtPower;
        statsPower[1].fillAmount = Save.manaPower;
        statsPower[2].fillAmount = Save.staminaPower;
    }
    public void Deeds()
    {
        if(objects[4].activeSelf|| objects[5].activeSelf)
        {
            for (int i = 4; i < 7; i++)
            {
                objects[i].SetActive(false);
            }
        }
        objects[7].SetActive(true);
    }
    public void Inventory()
    {
        for (int i = 5; i < 7; i++)
        {
            objects[i].SetActive(false);
        }
        objects[4].SetActive(true);
        if (objects[7].activeSelf)
        {
            objects[7].SetActive(false);
        }
    }
    public void RemoveIcon(int j)
    {
        for (int i = 0; i < max3; i++)
        {
            if(emptySlots[i].sprite == icons[j])
            {
                max3 = i;
                emptySlots[i].sprite = icons[0];
                emptySlots[i].transform.GetComponent<Hint_Message>().objectType = 0;
            }
        }
        max3 = emptySlots.Length;
    }
}
