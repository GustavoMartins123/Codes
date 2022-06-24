using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Inimigo01
{
    private string estado = "estado1";
    private Rigidbody2D rig;
    private float limiteHorizontal = 6f;
    private bool direita = true;
    [SerializeField] private Transform posicaoTiro1;
    [SerializeField] private Transform posicaoTiro2;
    private float tiroEspera2 = 1f;
    [SerializeField] private GameObject meuTiro2;
    private float delayTiro = 0.7f;
    [SerializeField] private string[] estados;
    private float esperaEstado = 10f;
    [SerializeField] private GameObject explosao;
    [SerializeField] private Image barraVida;
    [SerializeField] private int vidaMaxima = 500;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tiroEspera = Random.Range(0.5f, 2f);
        //Dando a minha vida inicial
        vida = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        TrocaEstado();
        switch (estado) 
        {
            case "estado1":
                Estado1();
                Tiro1();
                break;
            case "estado2":
                Estado2();
                break;
            case "estado3":
                Estado1();
                Estado3();
                break;
        }
        tiroEspera -= Time.deltaTime;
        if (vida <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosao, transform.position, transform.rotation);
            var gerador = FindObjectOfType<GeradorInimigos>();
            gerador.GanharPontos(pontos);
        }
        //Garantindo que retorne o valor em float
        barraVida.fillAmount = ((float)vida / (float)vidaMaxima);
        //Convertendo o valor fillamount em alguma coisa entre 0 e 255
        barraVida.color = new Color32(190,(byte)(barraVida.fillAmount * 255), 54, 255);
    }
    private void AumentaDificuldade()
    {
        //Checando se a vida é menor do que a metade
        if (vida <= vidaMaxima / 2)
        {
            tiroEspera = 0.8f;
        }
    }

    private void Estado1()
    {
        if (direita) 
        {
            //Indo para a direita e  esquerda
            rig.velocity = new Vector2(velocidade, 0f);
        }
        else 
        {
            rig.velocity = new Vector2(-velocidade, 0f);
        }
        //Mudando o valor da direita
        if(transform.position.x >= limiteHorizontal) 
        {
            direita = false;
        }
        else if(transform.position.x <= -limiteHorizontal) 
        {
            direita = true;
        }
    }
    private void Estado2() 
    {
        rig.velocity = Vector2.zero;
        if (tiroEspera2 <= 0)
        {
            Tiro2();
            tiroEspera2 = 0.5f;
        }
        else
        {
            tiroEspera2 -= Time.deltaTime;
        }
    }
    private void Estado3() 
    {
        if(tiroEspera <= 0) 
        {
            Tiro1();
        }
        else 
        {
            tiroEspera -= Time.deltaTime;
        }
        if (tiroEspera2 <= 0)
        {
            Tiro2();
            tiroEspera2 = delayTiro;
        }
        else
        {
            tiroEspera2 -= Time.deltaTime;
        }
    }
    private void Tiro1() 
    {
        tiroEspera -= Time.deltaTime;
        if (tiroEspera <= 0)
        {
            //Tiro da direita
            tiroEspera = 1.2f;
            AudioSource.PlayClipAtPoint(som, Vector3.zero);
            var tiro = Instantiate(meuTiro, posicaoTiro1.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;
            //Tiro da esquerda
            tiro = Instantiate(meuTiro, posicaoTiro2.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;
            AumentaDificuldade();
        }
    }
    private void Tiro2()
    {
        //Encontrando o player na cena
        var player = FindObjectOfType<PlayerController>();
        //Atirar somente se o player existir
        if (player)
        {
            var tiro1 = Instantiate(meuTiro2, tl.position, transform.rotation);
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
            AudioSource.PlayClipAtPoint(som, Vector3.zero);
        }
    }
    private void TrocaEstado() 
    {
        if(esperaEstado <= 0f) 
        {
            int indiceEstado = Random.Range(0, estados.Length);
            //Escolhendo o meu novo estado
            estado = estados[indiceEstado];
            esperaEstado = 10f;
        }
        else 
        {
            esperaEstado -= Time.deltaTime;
        }
    }
}
