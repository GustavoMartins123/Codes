using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamante : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool visivel = GetComponent<SpriteRenderer>().isVisible;
        if (visivel == true)
        {
            var Player = FindObjectOfType<PlayerController>();
            if (Player) 
            {
                anim.enabled = true;
            }
        }
        else 
        {
            anim.enabled = false;
        }
    }
}
