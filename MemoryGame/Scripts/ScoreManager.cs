using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int timeForLevelComplete = 60;
    Coroutine timer;
    //[SerializeField] Image image;
    [SerializeField] Text time;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        timer = StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        int temp = timeForLevelComplete;
        time.text = timeForLevelComplete.ToString();

        while (temp>0)
        {
            temp--;
            yield return new WaitForSeconds(1);
            //image.fillAmount = temp / (float)timeForLevelComplete;
            time.text = temp.ToString();
        }
        if(temp <= 0)
        {
            GameManager.instance.GameOver();
        }

    }

    public void StopTime()
    {
        StopCoroutine(timer);
    }
}
