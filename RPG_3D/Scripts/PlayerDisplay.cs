using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public static PlayerDisplay instance;
    public GameObject[] characters;
    public GameObject[] players;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        players[Save.pchar].SetActive(true);
    }
}
