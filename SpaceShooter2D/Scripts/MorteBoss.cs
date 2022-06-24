using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteBoss : MonoBehaviour
{
    [SerializeField] private Transform posicao1;
    [SerializeField] private Transform posicao2;
    [SerializeField] private Transform posicao3;
    [SerializeField] private GameObject explosao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Morrendo() 
    {
        GameObject explosao = Instantiate(this.explosao, posicao1.position, transform.rotation);
        Destroy(explosao,0.6f);
        explosao = Instantiate(this.explosao, posicao2.position, transform.rotation);
        Destroy(explosao, 0.6f);
        explosao = Instantiate(this.explosao, posicao3.position, transform.rotation);
        Destroy(explosao, 0.6f);
    }
}
