using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float velocidade = 10f;
    [SerializeField] private GameObject TiroObject;
    [SerializeField] private GameObject TiroObject2;
    [SerializeField] private Transform tl;
    [SerializeField] private int vida = 4;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject explosao;
    private float tiroEspera;
    [SerializeField] private float xlimite;
    [SerializeField] private float ylimite;
    [SerializeField] private int levelTiro = 1;
    [SerializeField] private GameObject escudo;
    private float escudoEspera;
    private GameObject escudoAtual;
    [SerializeField] private int qtdEscudo = 3;
    [SerializeField] private Text textoVida;
    [SerializeField] private Text textoEscudo;
    [SerializeField] private AudioClip som;
    [SerializeField] private AudioClip somEscudo;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();
        tiroEspera -= Time.deltaTime;
        escudoEspera -= Time.deltaTime;
        Atirando();
        if (vida <= 0)
        {
            rig.velocity = Vector2.zero;
            Destroy(gameObject);
            Instantiate(explosao, transform.position, transform.rotation);
            FindObjectOfType<Manager>().GameOverlay2();
        }
        CriaEscudo();
    }

    private void Atirando()
    {
        if (Input.GetButton("Fire1"))
        {
            if (tiroEspera <= 0)
            {
                switch (levelTiro)
                {
                    case 1:
                        CriaTiro(TiroObject, tl.position);
                        break;
                    case 2:
                        Vector3 posicao = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        posicao = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        break;
                    case 3:
                        CriaTiro(TiroObject, tl.position);
                        posicao = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        posicao = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        break;
                    case 4:
                        CriaTiro(TiroObject2, tl.position);
                        posicao = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        posicao = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject, posicao);
                        break;
                    case 5:
                        CriaTiro(TiroObject2, tl.position);
                        posicao = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject2, posicao);
                        posicao = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.2f, 0f);
                        CriaTiro(TiroObject2, posicao);
                        break;
                }

            }
        }
    }
    private void CriaTiro(GameObject tiroCriado, Vector3 posicao) 
    {
        GameObject tiro = Instantiate(tiroCriado, posicao, transform.rotation);
        AudioSource.PlayClipAtPoint(som, Vector3.zero);
        tiroEspera = 0.1f;
    }
    public void LevarDanoPlayer(int dano)
    {
        if(vida > 0) 
        {
            vida -= dano;
            textoVida.text = vida.ToString();
        }
    }
    private void Movendo() 
    {
        //Pegando o Input Horizontal
        float horizontal = Input.GetAxis("Horizontal");
        //Pegando o Input Vertical
        float Vertical = Input.GetAxis("Vertical");
        Vector2 minhaVelocidade = new Vector2(horizontal, Vertical);
        //Normalizando
        minhaVelocidade.Normalize();
        //Passando a minha velocidade para o meu rig
        rig.velocity = minhaVelocidade * velocidade;
        //Limitando a posição na tela
        float meux = Mathf.Clamp(transform.position.x, -xlimite, xlimite);
        float meuy = Mathf.Clamp(transform.position.y, -ylimite, ylimite);
        //Aplicando o meuX e o meuY a minha posição
        transform.position = new Vector3(meux, meuy, transform.position.z);
    }
    private void CriaEscudo()
    {
        if(qtdEscudo > 0) 
        {
            if (Input.GetButtonDown("shield"))//Ou poderia colocar && para verificar
            {
                if (!escudoAtual)
                {
                    escudoAtual = Instantiate(escudo, transform.position, transform.rotation);
                    qtdEscudo--;
                    textoEscudo.text = qtdEscudo.ToString();
                    AudioSource.PlayClipAtPoint(somEscudo, Vector3.zero);
                }
            }
        }
        //Fazendo o escudo seguir o player SE o escudoAtual existe
        if (escudoAtual)
        {
            escudoAtual.transform.position = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power")) 
        {
            if(levelTiro < 5) 
            {
                levelTiro++;
            }
            Destroy(collision.gameObject);
        }
    }
}
