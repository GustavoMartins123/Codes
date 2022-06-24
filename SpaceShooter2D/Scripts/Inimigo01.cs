using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo01 : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] protected float velocidade = 1f;
    [SerializeField] protected GameObject meuTiro;
    [SerializeField] protected float tiroEspera;
    [SerializeField] protected float velocidadeTiro = -6;
    //Pegando a posição do transform do meuTiro
    [SerializeField] protected Transform tl;
    [SerializeField] protected int vida = 2;
    [SerializeField] protected Animator anim;
    [SerializeField] private GameObject explosao;
    [SerializeField] protected int pontos = 100;
    [SerializeField] protected GameObject powerUP;
    [SerializeField] protected float itemRate = 0.99f;
    [SerializeField] protected AudioClip som;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(0f, -velocidade);
        tiroEspera = Random.Range(0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Pegando informações do meu filho
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;
        if(visivel == true) 
        {
            var Player = FindObjectOfType<PlayerController>();
            if (Player) 
            {
                //Espera do tiro
                tiroEspera -= Time.deltaTime;
                if (tiroEspera <= 0)
                {
                    tiroEspera = Random.Range(1.5f, 2f);
                    var tiro = Instantiate(meuTiro, tl.position, transform.rotation);
                    tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;
                    AudioSource.PlayClipAtPoint(som, Vector3.zero);
                }
            }
        }
    }
    public void LevarDano(int dano) 
    {
        if (transform.position.y < 5.50f)
        {
            vida -= dano;
            anim.SetTrigger("Hit");
            if (vida <= 0)
            {
                velocidade = 0f;
                Destroy(gameObject);
                Instantiate(explosao, transform.position, transform.rotation);
                //Ganhando pontos se o gerador for valido
                var gerador = FindObjectOfType<GeradorInimigos>();
                if (gerador) 
                {
                    gerador.GanharPontos(pontos);
                }
                //Dropando se a variavel powerUP for valida
                if (powerUP) 
                {
                    DropaItem();
                }
            }
        }
    }
    //Evento de quando eu me destruo
    private void OnDestroy()
    {
        var gerador = FindObjectOfType<GeradorInimigos>();
        //SE o gerador existe, ele executa
        if (gerador)
        {
            gerador.DiminuiInimigo();
        }
    }

    private void DropaItem()
    {
        //Calculando a chance de dropar o item
        float chance = Random.Range(0f, 1f);
        if(chance > itemRate) 
        {
            GameObject pu = Instantiate(powerUP, transform.position, transform.rotation);
            Destroy(pu, 6f);
            //Dando uma direção aleatoria
            Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            pu.GetComponent<Rigidbody2D>().velocity = dir;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Parede")) 
        {
            Destroy(gameObject);
            DropaItem();
            Instantiate(explosao, transform.position, transform.rotation);
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerController>().LevarDanoPlayer(2);
            Instantiate(explosao, transform.position, transform.rotation);
            DropaItem();
        }
    }
}
