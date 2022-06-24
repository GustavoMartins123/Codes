using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Riddle2D : MonoBehaviour
{
    public List<GameObject> pearlList = new List<GameObject>();
    public int pearlAmount = 4;
    List<GameObject> riddleList = new List<GameObject>();
    public List<GameObject> slotList = new List<GameObject>();
    [SerializeField] Transform parent;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateTheRiddle();
    }

    void CreateTheRiddle()
    {
        for (int i = 0; i < pearlAmount; i++)
        {
            int num = Random.Range(0, pearlList.Count);
            riddleList.Add(pearlList[num]);
            GameObject pearl = Instantiate(pearlList[num], slotList[i].transform, false);
            pearl.transform.position = pearl.transform.parent.position;
            pearl.GetComponent<Drag_2D>().enabled = false;
        }
    }

    public void CheckRiddle(int[] ids, Dragging_Box sender)
    {
        int[] places1 = new int[4] {-1,-1,-1,-1 };
        int[] places2 = new int[4] { -1, -1, -1, -1 };

        int exactMAtches = 0;
        int halfMAtches = 0;

        //BlackCheck
        for (int i = 0; i < 4; i++)
        {
            if(ids[i]== riddleList[i].GetComponent<Drag_2D>().pearl_Id)
            {
                exactMAtches++;
                sender.CreateHint(2);
                places1[i] = 1;
                places2[i] = 1;
            }
        }

        if (exactMAtches == 4)
        {
            //Win
            anim.SetTrigger("WinLose");
            GameManager.instance.WinCondition();
            return;
        }

        //White Check
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(i!=j && (places1[i] != 1) && (places2[j] != 1))
                {
                    if (ids[i] == riddleList[j].GetComponent<Drag_2D>().pearl_Id)
                    {
                        halfMAtches++;
                        sender.CreateHint(1);
                        places1[i] = 1;
                        places2[j] = 1;
                        break;
                    }
                }
            }
        }
        //UpdateTrys
        GameManager.instance.SetTrys();

    }
}
