using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXplosaoController : MonoBehaviour
{
    [SerializeField] private AudioClip meuSom;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.y > -6.50f) 
        {
            AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Morrendo() 
    {
        Destroy(gameObject);
        
    }
}
