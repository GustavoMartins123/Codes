using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string cena;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool tenhoDestino() 
    {
        return cena != "";
    }
    public void Abrindo() 
    {
        
        anim.SetTrigger("Abri");
        

    }
    public void TrocaCena() 
    {
        
        FindObjectOfType<GameManager>().TrocandoDeCena(cena);
        
    }
}
