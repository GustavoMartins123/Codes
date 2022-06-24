using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Dragging_Box> tryList = new List<Dragging_Box>();
    int currentTurn = 0;
    public int maxTurn;
    public GameObject Spacer;
    [SerializeField] Animator anim;

    //
    [SerializeField] Text winLose;
    [SerializeField]Text timerText, turnsText;
    [SerializeField] GameObject panelWinLose;
    int playTime, seconds, minutes;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetDraggingBox();
        UpdateTrys();
        StartCoroutine("PlayTime");
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateTrys()
    {
        tryList[currentTurn].SetActive(true);
    }

    void GetDraggingBox()
    {
        Dragging_Box[] allChildren = Spacer.GetComponentsInChildren<Dragging_Box>();
        tryList.AddRange(allChildren);
        tryList.Reverse();
        maxTurn = tryList.Count;
    }
    public void SetTrys()
    {
        currentTurn++;
        if (currentTurn < maxTurn)
        {
            UpdateTrys();
            tryList[currentTurn-1].SetActive(false);
        }
        else
        {
            //Lose
            LoseCondition();
        }
    }

    void LoseCondition()
    {
        anim.SetTrigger("WinLose");
        StartCoroutine(Lose());
        tryList[currentTurn-1].SetActive(false);
    }

    public void WinCondition()
    {
        StartCoroutine(Win());
        tryList[currentTurn].SetActive(false);
    }

    IEnumerator Lose()
    {
        StopCoroutine("PlayTime");
        yield return new WaitForSeconds(2.5f);
        panelWinLose.SetActive(true);
        winLose.text = "You Lose ";
    }
    IEnumerator Win()
    {
        StopCoroutine("PlayTime");
        yield return new WaitForSeconds(2.5f);
        panelWinLose.SetActive(true);
        winLose.text = "You Win ";
    }

    void UpdateText()
    {
        turnsText.text = "Turns Taken: " + currentTurn;
    }
    IEnumerator PlayTime()
    {
        while (true)
        {
            playTime++;
            seconds = playTime% 60;
            minutes = playTime / 60 % 60;
            timerText.text = "Time Taken: " +minutes.ToString("D2") + ":" + seconds.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}
