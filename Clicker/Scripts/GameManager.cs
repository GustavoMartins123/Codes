using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [System.Serializable]
    public class AnyBook 
    {
        [HideInInspector]public int BookAmount;
        public Icons icons;
        public bool unlocked;
        [HideInInspector] public bool instace;
        [HideInInspector] public Item holder;
    }
    public List<AnyBook> bookList = new List<AnyBook>();

    public float money;
    public Text totalMg;
    public Text totalMgsText;
    public Text totalMgPerClick;
    public GameObject itemholderUI;
    public Transform grid;
    public int e = 0;
    [SerializeField] GameObject saveGame;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("IdleSave")) 
        {
            LoadTheGame();
        }
        else 
        {
            FillList();
        }
        UpdateMG();
        MgsCalculate();
        Click();
        SaveTheGame();
        StartCoroutine(Tick());
        AutoSave();
    }
    IEnumerator Tick() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            foreach(AnyBook book in bookList) 
            {
                if(book.BookAmount > 0) 
                {
                    money += book.icons.CalculateIncome(book.BookAmount);
                    money = (float)Mathf.Round(money * 100) / 100;
                    UpdateMG();
                }
            }
        }

    }
    
    void FillList() 
    {
        for(int i = 0; i < bookList.Count; i++) 
        {
            if (bookList[i].unlocked) 
            {
                if(bookList[i].BookAmount> 0|| bookList[i].instace) 
                {
                    continue;
                }
                GameObject itemHolder = Instantiate(itemholderUI, grid, false) as GameObject;
                bookList[i].holder = itemHolder.GetComponent<Item>();
                if(bookList[i].BookAmount > 0) 
                {
                    bookList[i].holder.itemImage.sprite = bookList[i].icons.sprite;
                    bookList[i].holder.itemName.text = bookList[i].icons.Name;
                    bookList[i].holder.amountText.text = "Amount: " + bookList[i].BookAmount.ToString();
                    bookList[i].holder.mgsText.text = "MGS: " + bookList[i].icons.CalculateIncome(bookList[i].BookAmount).ToString("N2");
                    bookList[i].holder.costText.text = "Cost: " + bookList[i].icons.CalculateCost(bookList[i].BookAmount).ToString("N2");
                }
                else
                {
                    bookList[i].holder.itemImage.sprite = bookList[i].icons.unknowIcon;
                    bookList[i].holder.itemName.text = "?????????";
                    bookList[i].holder.amountText.text = "Amount: " + bookList[i].BookAmount.ToString();
                    bookList[i].holder.mgsText.text = "MGS: " + bookList[i].icons.CalculateIncome(bookList[i].BookAmount).ToString("N2");
                    bookList[i].holder.costText.text = "Cost: " + bookList[i].icons.CalculateCost(bookList[i].BookAmount).ToString("N2");
                }

                bookList[i].holder.buyButton.id = i;
                bookList[i].instace = true;
            }
        }
    }
    public void BuyItem(int id) 
    {
        if(money < bookList[id].icons.CalculateCost(bookList[id].BookAmount)) 
        {
            return;
        }
        else if (money > bookList[id].icons.CalculateCost(bookList[id].BookAmount))
        {
            e++;
        }
        money -= bookList[id].icons.CalculateCost(bookList[id].BookAmount);

        if(bookList[id].BookAmount < 1) 
        {
            bookList[id].holder.itemImage.sprite = bookList[id].icons.sprite;
            bookList[id].holder.itemName.text = bookList[id].icons.Name;
        }

        bookList[id].BookAmount++;
        bookList[id].holder.amountText.text = "Amount: " + bookList[id].BookAmount.ToString();
        bookList[id].holder.mgsText.text = "MGS: " + bookList[id].icons.CalculateIncome(bookList[id].BookAmount).ToString("N2");
        bookList[id].holder.costText.text = "Cost: " + bookList[id].icons.CalculateCost(bookList[id].BookAmount).ToString("N2");
        if (id< bookList.Count-1 && bookList[id].BookAmount>0) 
        {
            bookList[id + 1].unlocked = true;
            FillList();
        }
        if (id < bookList.Count && bookList[id].BookAmount > 4 && e > 4)
        {
            BookMaster.level++;
            PlayerPrefs.SetInt("level", BookMaster.level);
            e =0;
            Click();
        }
        MgsCalculate();
        UpdateMG();
    }
    public void AddMoney(int clickAmount)
    {
        money += clickAmount;
        UpdateMG();
    }
    void UpdateMG() 
    {
        totalMg.text ="Total MG: "+ money.ToString("N2");
    }
    void MgsCalculate() 
    {
        float allMgc = 0;
        foreach(AnyBook a in bookList) 
        {
            if (a.BookAmount > 0)
            {
                allMgc += a.icons.CalculateIncome(a.BookAmount);
                totalMgsText.text = "MGS: " + allMgc.ToString("N2");
            }
        }
        if(allMgc == 0) 
        {
            totalMgsText.text = "MGS: " + allMgc.ToString("N2");
        }
    }
    void Click() 
    {
        totalMgPerClick.text = "MG per Click: " + BookMaster.level;
    }

    void SaveTheGame() 
    {
        Save.Save01(bookList[0].BookAmount, bookList[1].BookAmount, bookList[2].BookAmount, bookList[3].BookAmount, bookList[4].BookAmount, bookList[5].BookAmount, bookList[6].BookAmount, bookList[7].BookAmount, bookList[8].BookAmount, bookList[9].BookAmount, bookList[10].BookAmount, bookList[11].BookAmount, money);
    }
    void AutoSave() 
    {
        SaveTheGame();
        Invoke("AutoSave", 60f);
        saveGame.SetActive(true);
        Invoke("DisableSave", 1.2f);
    }
    public void SaveOnBuy() 
    {
        SaveTheGame();
        PlayerPrefs.SetInt("i", e);
    }

    void LoadTheGame() 
    {
        if (PlayerPrefs.HasKey("IdleSave")) 
        {
            string data = Save.Load();
            string[] stringList = data.Split("|" [0]);
            for(int i =0; i < stringList.Length-1; i++) 
            {
                int temp = int.Parse(stringList[i]);
                bookList[i].BookAmount = temp;
                if(temp > 0) 
                {
                    if(i+1< bookList.Count) 
                    {
                        bookList[i + 1].unlocked = true;
                    }
                    FillItem(i);
                }
            }
            money = float.Parse(stringList[12]);
            FillList();
            UpdateMG();
            MgsCalculate();
        }
        if (PlayerPrefs.HasKey("level")) 
        {
            BookMaster.level = PlayerPrefs.GetInt("level");
        }
        else 
        {
            BookMaster.level = 1;
        }
        if (PlayerPrefs.HasKey("i")) 
        {
            e = PlayerPrefs.GetInt("i");
        }
        else 
        {
            e = 0;
        }
    }
    void FillItem(int i) 
    {
        if (bookList[i].unlocked) 
        {
            GameObject itemHolder = Instantiate(itemholderUI, grid, false) as GameObject;
            bookList[i].holder = itemHolder.GetComponent<Item>();
            if(bookList[i].BookAmount > 0) 
            {
                if (bookList[i].BookAmount > 0)
                {
                    bookList[i].holder.itemImage.sprite = bookList[i].icons.sprite;
                    bookList[i].holder.itemName.text = bookList[i].icons.Name;
                    bookList[i].holder.amountText.text = "Amount: " + bookList[i].BookAmount.ToString();
                    bookList[i].holder.mgsText.text = "MGS: " + bookList[i].icons.CalculateIncome(bookList[i].BookAmount).ToString("N2");
                    bookList[i].holder.costText.text = "Cost: " + bookList[i].icons.CalculateCost(bookList[i].BookAmount).ToString("N2");
                }
                else
                {
                    bookList[i].holder.itemImage.sprite = bookList[i].icons.unknowIcon;
                    bookList[i].holder.itemName.text = "?????????";
                    bookList[i].holder.amountText.text = "Amount: " + bookList[i].BookAmount.ToString();
                    bookList[i].holder.mgsText.text = "MGS: " + bookList[i].icons.CalculateIncome(bookList[i].BookAmount).ToString("N2");
                    bookList[i].holder.costText.text = "Cost: " + bookList[i].icons.CalculateCost(bookList[i].BookAmount).ToString("N2");
                }

                bookList[i].holder.buyButton.id = i;
                bookList[i].instace = true;
            }
        }
    }
    void DisableSave() 
    {
        saveGame.SetActive(false);
    }
}
