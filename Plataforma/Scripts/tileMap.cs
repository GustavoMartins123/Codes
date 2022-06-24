using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class tileMap : MonoBehaviour
{
    private Grid gd;
    private Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        gd = GetComponent<Grid>();
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool visivel = GetComponent<Renderer>().isVisible;
        if (visivel == true)
        {
            var Player = FindObjectOfType<PlayerController>();
            if (Player)
            {
                gd.enabled = true;
                rd.enabled = true;
            }
        }
        else
        {
            gd.enabled = false;
            rd.enabled = false;
        }
    }
}
