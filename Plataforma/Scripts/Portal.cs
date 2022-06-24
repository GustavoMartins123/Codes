using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Rigidbody2D Player;
    private GameObject inicialPos;

    private bool restartPlayer;

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        inicialPos = GameObject.Find("PortalCheck");
    }

    void Update()
    {
        Restart();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Portal") == true)
        {
            restartPlayer = true;
        }
    }
    private void Restart()
    {
        if (restartPlayer == true)
        {
            Player.transform.position = new Vector2(inicialPos.transform.position.x, inicialPos.transform.position.y);
            restartPlayer = false;
        }
    }
}
