using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorInimigos : MonoBehaviour
{
    [SerializeField] private GameObject[]Inimigos;

    private int pontos = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int baseLevel = 1000;
    private float esperaInimigo = 0f;
    [SerializeField] private int qtdInimigo = 0;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform tl;
    [SerializeField] private float tempoEspera = 2f;
    private bool animacaoBoss = false;
    [SerializeField] private Text textoPontos;
    [SerializeField] private AudioClip musica;
    [SerializeField] private AudioSource musica2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(level < 10) 
        {
            GeraInimigo();
        }
        else 
        {
            GeraBoss();
        }
    }

    private void GeraBoss()
    {
        if(qtdInimigo <= 0 && tempoEspera > 0) 
        {
            tempoEspera -= Time.deltaTime;
        }
        //Criando o metodo para gerar o boss
        //Instanciando a anima��o
        if (!animacaoBoss && tempoEspera <= 0)
        {
            Instantiate(boss, tl.position, transform.rotation);
            //Avisando que ja fiz a anima��o
            animacaoBoss = true;
            musica2.clip = musica;
            musica2.Play();

        }
    }

    //Diminuindo a quantidade de inimigos
    public void DiminuiInimigo() 
    {
        qtdInimigo--;
    }
    //Metodo para checar se a posi��o est� livre
    private bool ChecaLugar(Vector3 posicao, Vector3 size) 
    {
        //Estou vendo se na posi��o tem algum colisor 2d
        Collider2D hit = Physics2D.OverlapBox(posicao, size, 0f);
        //Se o hit � Null= N�o houve colis�o, e retorna false
        //Ent�o posso criar um inimigo
        if(hit != null) 
        {
            return true;
            //Houve colis�o
        }
        //Se o hit N�O � Null = Houve colis�o, e retorna true
        else 
        {
            return false;
            //N�o houve colis�o
        }

    }
    private void GeraInimigo() 
    {
        if(esperaInimigo > 0f && qtdInimigo <= 0) 
        {
            esperaInimigo -= Time.deltaTime;
        }
        if (esperaInimigo <= 0f && qtdInimigo <= 0)
        {
            int quantidade = level * 4;
            int tentativas = 0;
            //Criando varios inimigos de uma vez
            while(qtdInimigo < quantidade) 
            {
                //Fazendo ele sair do la�o de repeti��o SE ele repetir muitas vezes
                //SE ele tentou mais de 200 vezes, ele desiste
                tentativas++;
                if(tentativas > 200) 
                {
                    break;
                }
                GameObject InimigoCriado;
                //Decidindo qual inimigo vai ser criado com base no level
                float chance = Random.Range(0f, level);
                if (chance > 2)
                {
                    InimigoCriado = Inimigos[1];
                }
                else
                {
                    InimigoCriado = Inimigos[0];
                }
                //Definindo a posi��o na qual o inimigo deve ser gerado
                Vector3 posicao = new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 16f));
                //Eu preciso checar se a posi��o est� livre
                bool colisao = ChecaLugar(posicao, InimigoCriado.transform.localScale);
                //Criando os inimigos SE n�o houve colis�o
                //SE houve colis�o vou pular essa repeti��o
                if (colisao) 
                {
                    //Faz com que o la�o de repeti��o v� para a proxima repeti��o
                    continue;
                }
                //Gerando um Inimigo
                Instantiate(InimigoCriado, posicao, Quaternion.identity);
                //Aumentando a quantidade de inimigos
                qtdInimigo++;
                //Reiniciando a Espera
                esperaInimigo = tempoEspera;
            }
        }
    }
    //Ganhando Pontos
    public void GanharPontos(int pontos) 
    {
        this.pontos += pontos * level;
        textoPontos.text = this.pontos.ToString();
        //Ganhando level SE os pontos forem maior do que a baseLevel * level
        if (this.pontos > baseLevel ) 
        {
            level++;
            baseLevel *= 2;
        }
        Debug.Log(this.pontos);
    }
}
