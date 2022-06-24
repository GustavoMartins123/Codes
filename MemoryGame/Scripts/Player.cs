using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.HasPicked() && ready == true && !GameManager.instance.HasGameOver())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Card currentCard = hit.transform.GetComponent<Card>();
                currentCard.FlipOpen(true);
                currentCard.GetComponent<Collider>().enabled = false;
                GameManager.instance.AddCardToPickList(currentCard);
            }
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(7f);
        ready = true;
    }
}
