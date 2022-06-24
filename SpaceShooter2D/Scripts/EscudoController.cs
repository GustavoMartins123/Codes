using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoController : MonoBehaviour
{
    [SerializeField] private AudioClip meuSom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Morrendo()
    {
        AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
        Destroy(gameObject);
    }
}

