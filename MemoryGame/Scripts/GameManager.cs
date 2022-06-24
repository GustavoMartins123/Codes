using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    bool picked;
    int pairs;
    int pairCounter;
    public bool hideMatches;
    bool gameOver;
    List<Card> pickedCards = new List<Card>();
    [SerializeField] GameObject panel_GameOver, panel_Win, effect;
    private void Awake()
    {
        instance = this;
        
    }
    public void AddCardToPickList(Card card)
    {
        if (pickedCards.Contains(card))
        {
            return;
        }
        pickedCards.Add(card);
        if(pickedCards.Count == 2)
        {
            picked = true;
            StartCoroutine(CheckMath());
        }
    }

    IEnumerator CheckMath()
    {
        yield return new WaitForSeconds(1.5f);
        if(pickedCards[0].GetCardId() == pickedCards[1].GetCardId())
        {
            if (hideMatches)
            {
                pickedCards[0].gameObject.SetActive(false);
                pickedCards[1].gameObject.SetActive(false);

            }
            else
            {
                pickedCards[0].GetComponent<Collider>().enabled = false;
                pickedCards[1].GetComponent<Collider>().enabled = false;
                pickedCards[0].gameObject.GetComponentInChildren<ParticleSystem>().Play();
                pickedCards[1].gameObject.GetComponentInChildren<ParticleSystem>().Play();

            }

            pairCounter++;
            CheckWin();

        }
        else
        {
            pickedCards[0].FlipOpen(false);
            pickedCards[1].FlipOpen(false);
            pickedCards[0].GetComponent<Collider>().enabled = true;
            pickedCards[1].GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(1.5f);
        }
        picked = false;
        pickedCards.Clear();
    }

    void CheckWin()
    {
        if(pairs == pairCounter)
        {
            ScoreManager.instance.StopTime();
            effect.SetActive(true);
            StartCoroutine(Corrotina_Win());
        }
    }
    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(Corrotina_GameOver());
    }

    IEnumerator Corrotina_GameOver()
    {
        yield return new WaitForSeconds(.4f);
        panel_GameOver.SetActive(true);
    }
    IEnumerator Corrotina_Win()
    {
        yield return new WaitForSeconds(.4f);
        panel_Win.SetActive(true);
    }
    public bool HasPicked()
    {
        return picked;
    }
    public bool HasGameOver()
    {
        return gameOver;
    }
    public void SetPairs(int pairAmount)
    {
        pairs = pairAmount;
    }
}
