using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evento_Arvore : MonoBehaviour
{
    public delegate void _Arvore();
    public static event _Arvore arvore;
    public Transform arv;
    private void Start()
    {
        arv = GetComponent<Transform>();
    }

    public void OnMouseDown()
    {
        arvore();
    }
}
