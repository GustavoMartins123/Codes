using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo02 : Inimigo01
{
    private Rigidbody2D rig1;
    [SerializeField] private float yMax = 2.5f;
    [SerializeField] private float xV= 0;
    private bool possoMover = true;
    [SerializeField] private GameObject explosao;
    // Start is called before the first frame update
    void Start()
    {
        rig1 = GetComponent<Rigidbody2D>();
        rig1.velocity = Vector2.down * velocidade;
        tiroEspera = Random.Range(0.5f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;
        if (visivel == true)
        {
            //Encontrando o player na cena
            var player = FindObjectOfType<PlayerController>();
            //Atirar somente se o player existir
            if (player) 
            {
                //Espera do tiro
                tiroEspera -= Time.deltaTime;
                if (tiroEspera <= 0)
                {
                    var tiro1 = Instantiate(meuTiro, tl.position, transform.rotation);
                    //Encontrando o valor da direção
                    Vector2 direcao = player.transform.position - tiro1.transform.position;
                    //Normalizando a velocidade dele
                    direcao.Normalize();
                    //Dando a direção e velocidade do meuTiro
                    tiro1.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeTiro;
                    //Dando o angulo que o tiro tem que estar
                    float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                    //Passando o angulo
                    tiro1.transform.rotation = Quaternion.Euler(0f, 0f, angulo + 90f);
                    tiroEspera = Random.Range(1.5f, 4f);
                    AudioSource.PlayClipAtPoint(som, Vector3.zero);
                }
            }
        }
        if (vida <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosao, transform.position, transform.rotation);
            var gerador = FindObjectOfType<GeradorInimigos>();
            gerador.GanharPontos(pontos);

        }
        //checando se cheguei no meio da tela e se posso me mover
        if (transform.position.y < yMax && possoMover) 
        {
            if(transform.position.x > xV) 
            {
                rig1.velocity = new Vector2(velocidade * -1f, -velocidade);
                //Falando que não posso me mover mais
                possoMover = false;
            }
            else
            {
                rig1.velocity = new Vector2(velocidade, -velocidade);
                //Falando que não posso me mover mais
                possoMover = false;
            }

        }
    }
    private void OnDestroy()
    {
        var gerador = FindObjectOfType<GeradorInimigos>();
        if (gerador) 
        {
            gerador.DiminuiInimigo();
        }
    }
}
