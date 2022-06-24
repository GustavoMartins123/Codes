using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkScripts : MonoBehaviour
{
    [SerializeField] GameObject iconChat, chatBox, cursor;
    [SerializeField] GameObject[] shop;
    Transform player;
    bool puts;
    bool shopingIsOpen;
    [SerializeField] Text text;
    public int myNumber;
    public string shopMessage;
    public bool[] Npcs;
    private void OnEnable()
    {
        if(player == null)
        {
            text.text = "Good morning " + Save.pname + ", what do you want?";
            //StartCoroutine(PlayerTransform());
            player = PlayerDisplay.instance.players[Save.pchar].transform;
        }

    }
    private void Update()
    {
        if (player)
        {
            Vector3 offset = player.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
            if (sqrLen < 5)
            {
                iconChat.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F) && shopingIsOpen == false) 
                {
                    
                    puts = !puts;
                    chatBox.SetActive(puts);
                    cursor.SetActive(puts);
                    Player.interaction = puts;
                }
            }
            else
            {
                iconChat.SetActive(false);
                chatBox.SetActive(false);
            }
        }
    }
    public void FirstQuestion()
    {
        text.text = shopMessage;//"Yeah, thanks for the question, do you need anything else?";
        if(Npcs[0] == true)
        {
            if (Open_Inventory.instance.objectives[3]==false && Open_Inventory.instance.dragon == false)
            {
                Open_Inventory.instance.objectivesReceived[0].interactable = false;
            }
            Open_Inventory.instance.messages[3].text = "Find the Dragon and Kill her";
            if (Open_Inventory.instance.objectives[3] && Open_Inventory.instance.objectivesReceived[0].interactable)
            {
                ReviveBoss.revive.Revive(0);
                Open_Inventory.instance.objectivesReceived[0].interactable = false;
            }
            Open_Inventory.instance.UpdateMessage(false, 3, 0);

        }
        else if(Npcs[1] == true)
        {
            if(Open_Inventory.instance.objectives[4] == false && Open_Inventory.instance.spider == false)
            {
                Open_Inventory.instance.objectivesReceived[1].interactable = false;
            }
            Open_Inventory.instance.messages[4].text = "Find the Spider and Kill her";
            if (Open_Inventory.instance.objectives[4] && Open_Inventory.instance.objectivesReceived[1].interactable)
            {
                
                ReviveBoss.revive.Revive(1);
                Open_Inventory.instance.objectivesReceived[1].interactable = false;
            }
            Open_Inventory.instance.UpdateMessage(false, 4, 0);

        }
        else if (Npcs[2] == true)
        {
            if(Open_Inventory.instance.objectives[5] == false)
            {
                Open_Inventory.instance.objectivesReceived[2].interactable = false;
            }
            Open_Inventory.instance.messages[5].text = "Get gold and buy some weapon from the blacksmith";
            if (Open_Inventory.instance.objectivesReceived[2].interactable && Open_Inventory.instance.objectives[5])
            {
                Open_Inventory.instance.UpdateMessage(false, 5, 0);
            }

        }
    }
    public void ExitDialogue()
    {
        Player.interaction = false;
        chatBox.SetActive(false);
        if(puts == true)
        {
            puts = false;
        }
        shop[myNumber].SetActive(false);
        cursor.SetActive(false);
        shopingIsOpen = false;
    }
    public void OpenShop()
    {
        text.text = "Select any items you want";
        shop[myNumber].SetActive(true);
        shopingIsOpen = true;
        shop[myNumber].GetComponent<BuyScript>().UpdateGold();
    }
    public void CloseShop()
    {
        text.text = "Good morning " + Save.pname + ", what do you want?";
        shop[myNumber].SetActive(false);
        shopingIsOpen = false;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iconChat.SetActive(true);
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iconChat.SetActive(false);
            chatBox.SetActive(false);
            player = null;
        }
    }*/
}
