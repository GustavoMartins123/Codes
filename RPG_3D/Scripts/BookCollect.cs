using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollect : MonoBehaviour
{
    [SerializeField] GameObject magicsUI, spellsUI;
    bool magicCollected = false;
    bool spellCollected = false;
    [SerializeField]bool magicBook = false;
    [SerializeField]bool spellsBook = false;
    private void Start()
    {
        if (magicBook)
        {
            magicsUI.SetActive(false);
        }
        if (spellsBook) 
        {
            spellsUI.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(magicBook == true)
            {
                if (magicCollected == false)
                {
                    magicsUI.SetActive(true);
                    magicCollected = true;
                    Destroy(gameObject);
                }
            }
            if (spellsBook == true)
            {
                if (spellCollected == false)
                {
                    spellsUI.SetActive(true);
                    spellCollected = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
