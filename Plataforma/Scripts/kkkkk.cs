using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kkkkk : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
                sprite.enabled = true;
            }
            else
            {
                sprite.enabled = false;
                anim.enabled = false;
            }
        }
    }
}
