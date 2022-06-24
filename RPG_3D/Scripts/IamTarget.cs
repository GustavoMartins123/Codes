using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamTarget : MonoBehaviour
{
    Outline outi;
    GameObject @object;
    public bool outlineOn;
    // Start is called before the first frame update
    void Start()
    {
        outi = GetComponent<Outline>();
        @object = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(outlineOn == false)
        {
            outlineOn = true;
            if(Player.enemy == @object)
            {
                outi.enabled = true;
            }
        }
        if (outlineOn == true)
        {
            if (Player.enemy != @object)
            {
                outi.enabled = false;
                outlineOn = false;
            }
        }
    }
}
