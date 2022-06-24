using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Dragging_Box : MonoBehaviour
{
    public GameObject protector;
    public bool isActive;
    public Button checkButton;
    public int[] pearlIds = new int[4];
    public Create_Riddle2D riddle;

    public GameObject hintBox;
    public GameObject blackHintPearl;
    public GameObject whiteHintPearl;
    // Start is called before the first frame update
    void Start()
    {
        checkButton.interactable = false;
    }

    public void SetId(int slot, int id)
    {
        pearlIds[slot] = id;
        checkButton.interactable = (!pearlIds.Contains(0) ? true : false);
        //The same
        /*if (pearlIds.Contains(0))
        {
            checkButton.interactable = false;
        }
        else
        {
            checkButton.interactable = true;
        }*/

        //PressingButton
    }

    public void CheckSolution()
    {
        riddle.CheckRiddle(pearlIds, this);
        checkButton.interactable = false;
    }
    public void CreateHint(int hint)
    {
        if(hint == 2)
        {
            Instantiate(blackHintPearl, hintBox.transform, false);
        }
        if(hint == 1)
        {
            Instantiate(whiteHintPearl, hintBox.transform, false);
        }
    }
    public void SetActive(bool active)
    {
        isActive = (active == true) ? true : false;
        protector.SetActive(!isActive);
    }
}
