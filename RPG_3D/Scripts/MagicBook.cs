using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBook : MonoBehaviour
{
    [SerializeField] Image magicIcon;
    [SerializeField] Text magicName;
    [SerializeField] Text description;
    public Sprite[] magicSprites;
    public string[] names;
    public string[] descriptions;
    public GameObject[] iconSets;
    public static int num;
    [SerializeField] GameObject panel;
    bool OpenPanel;
    CreatePotion potion;
    // Start is called before the first frame update
    void Start()
    {
        magicIcon.sprite = magicSprites[0];
        magicName.text = names[0];
        description.text = descriptions[0];
        iconSets[0].SetActive(true);
        potion = GetComponent<CreatePotion>();
        num = 0;
    }

    public void Next()
    {
        if (num == 5)
        {
            num = 0;
            iconSets[num].SetActive(true);
            iconSets[5].SetActive(false);
            magicIcon.sprite = magicSprites[num];
            magicName.text = names[num];
            description.text = descriptions[num];
            return;
        }
        iconSets[num].SetActive(false);
        num++;
        iconSets[num].SetActive(true);
        magicIcon.sprite = magicSprites[num];
        magicName.text = names[num];
        description.text = descriptions[num];
        potion.itemId++;
        potion.value =0;
        potion.thisValue =0;
    }
    public void Back()
    {
        if (num == 0)
        {
            num = 5;
            iconSets[num].SetActive(true);
            iconSets[0].SetActive(false);
            magicIcon.sprite = magicSprites[num];
            magicName.text = names[num];
            description.text = descriptions[num];
            return;
        }
        iconSets[num].SetActive(false);
        num--;
        iconSets[num].SetActive(true);
        magicIcon.sprite = magicSprites[num];
        magicName.text = names[num];
        description.text = descriptions[num];
        potion.itemId--;
        potion.value = 0;
        potion.thisValue = 0;
    }
    public void Open()
    {
        OpenPanel = !OpenPanel;
        panel.SetActive(OpenPanel);
    }
    public void Close()
    {
        //Because of Close button, but is irrelevant
        panel.SetActive(false);
        OpenPanel = false;
    }
 
}
