using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaPosicao : MonoBehaviour
{
    public bool colidiu;
    public List<Collider> colliders = new List<Collider>();
    private void FixedUpdate()
    {
        if(colliders.Count < 1) 
        {
            colidiu = false;
        }
        if(colliders.Count > 0) 
        {
            colidiu = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) 
        {
            colliders.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 7) 
        {
            colliders.Remove(other);
        }
    }
}
