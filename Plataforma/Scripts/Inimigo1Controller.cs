using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo1Controller : MonoBehaviour
{
    private Rigidbody2D meuRig;
    [SerializeField] private float velH = 3;
    private Animator anim;
    [SerializeField] private float espera = 3f;
    //Elementos do raycast
    private BoxCollider2D boxColl;
    [SerializeField] private LayerMask layerLevel;
    private bool morto = false;
    [SerializeField] private BoxCollider2D colisor;
    [SerializeField] private int vidaMax = 1;
    [SerializeField] private int vidaAtual = 1;
    [SerializeField] private GameObject diamante;
    private Spawner sp;
    [SerializeField] private Image barraVida;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        meuRig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        vidaAtual = vidaMax;
        sp = FindObjectOfType<Spawner>();
        sprite = GetComponent<SpriteRenderer>();
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
                barraVida.fillAmount = ((float)vidaAtual / (float)vidaMax);
                //Convertendo o valor fillamount em alguma coisa entre 0 e 255
                barraVida.color = new Color32(190, (byte)(barraVida.fillAmount * 255), 54, 255);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!morto)
        {
            Movendo();
        }
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
    public void morrendo() 
    {
        morto = true;
        meuRig.velocity = Vector2.zero;
        Destroy(gameObject, 1f);
        colisor.enabled = false;
    }
    public void RecebendoDano(int dano) 
    {
        vidaAtual -= dano;
        anim.SetTrigger("Hit");
        if (vidaAtual <= 0) 
        {
            morrendo();
            anim.SetTrigger("Die");
            Instantiate(diamante, transform.position, transform.rotation);
        }
    }

    private void Movendo()
    {
        if (isParede())
        {
            meuRig.velocity = new Vector2(meuRig.velocity.x * -1f, meuRig.velocity.y);
        }
        if (meuRig.velocity.x != 0)
        {
            //Ohando para onde estou indo
            transform.localScale = new Vector3(Mathf.Sign(meuRig.velocity.x) * -1, 1f, 1f);
        }
        if (espera <= 0)
        {
            int direcao = Random.Range(-1, 2);
            //Multiplicando a minha velocidade pela direção
            meuRig.velocity = new Vector2(velH * direcao, meuRig.velocity.y);
            espera = Random.Range(2f, 5f);
        }
        else
        {
            espera -= Time.deltaTime;
        }
        anim.SetBool("Correndo", meuRig.velocity.x != 0);
    }

    private bool isParede()
    {
        //Criar o meu raycast         //Pegando os limites do meu colisor
        var dir = new Vector2(Mathf.Sign(meuRig.velocity.x), 0f);
        bool parede = Physics2D.Raycast(boxColl.bounds.center, dir, 0.4f, layerLevel);
        return parede;
    }
    void Diminuindo()
    {
        sp.DiminuiInimigo();
    }
}
