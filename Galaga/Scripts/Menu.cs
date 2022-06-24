using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    [SerializeField]Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseOver()
    {
        anim.SetBool("over", true);
    }
    private void OnMouseExit()
    {
        anim.SetBool("over", false);
    }
}
