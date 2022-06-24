using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float velocidadePulo = 7f;
    [SerializeField] private float velocidadeMorte = 12f;
    private Animator anim;
    private int totalPulos = 1;
    [SerializeField] private int pulos = 1;
    //Elementos do raycast
    private BoxCollider2D boxColl;
    [SerializeField] private LayerMask layerLevel;
    [SerializeField] private int vida = 3;
    [SerializeField] private float invencivel = 0f;
    private bool morto = false;
    [SerializeField] private PortaController minhaPorta;
    private GameManager gm;
    private GameObject inicialPos;
    private GameObject inicialPos1;
    [SerializeField] private GameObject barra;
    [SerializeField] private Transform tl;

    private bool restartPlayer;
    private bool restartPlayer1;
    //Elementos do ataque
    [SerializeField] private Transform ataque;
    [SerializeField] private float alcanceAtaque = 0.5f;
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private float esperaAtaque = 1f;

    private bool IsPaused;
    [Header("Paineis e menu")]
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        gm = FindObjectOfType<GameManager>();
        vida = gm.GetVida();
        inicialPos = GameObject.Find("PortalCheck");
        inicialPos1 = GameObject.Find("PortalCheck2");
    }

    // Update is called once per frame
    void Update()
    {
        if (!morto) 
        {
            Movendo();
            Pulando();
            GerarDano();
            AbrindoPorta();
            Restart();
            Ataque();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseScreen();
                gm.Checar();
            }
        }
    }
    public void Recomeço() 
    {
        gm.GameOver();
    }

    private void GerarDano()
    {
        if (invencivel > 0)
        {
            invencivel -= Time.deltaTime;
        }
        if(vida <= 0) 
        {
            rig.velocity = new Vector2(0f, -velocidadeMorte);
            anim.SetBool("Correndo", false);
            
            morto = true;
            if (isGrounded()) 
            {
                anim.SetTrigger("vida");
            }
        }
    }

    private void FixedUpdate()
    {
        anim.SetBool("Pulando", isGrounded());
        if (isGrounded()) //&& rig.velocity.y < 0.1f    Caso eu queira arrumar o bug do pulo duplo
        {
            pulos = totalPulos;
        }
    }

    private void Movendo()
    {
        //Pegando o meu Input
        var Horizontal = Input.GetAxis("Horizontal") * velocidade;
        //Passando a minha velocidade para o meu rig
        rig.velocity = new Vector2(Horizontal, rig.velocity.y);
        //Ajustando a escala dele
        /*if (Horizontal > 0)
        {
            rig.velocity = new Vector2(velocidade, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (Horizontal < 0) 
        {
            rig.velocity = new Vector2(-velocidade, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }*/
        if(Horizontal!= 0) 
        {
            transform.localScale = new Vector3(Mathf.Sign(Horizontal), 1f, 1f);
            anim.SetBool("Correndo", true);
        }
        else 
        {
            anim.SetBool("Correndo", false);
        }
        //anim.SetBool("Correndo", Horizontal != 0)
    }
    private void Pulando() 
    {
        var Pulo = Input.GetButtonDown("Jump");
        //Definindo o parametro do velocidadePulo com base no meu Y do meu rig
        anim.SetFloat("velocidadePulo", rig.velocity.y);
        if (Pulo && pulos > 0) 
        {
            rig.velocity = new Vector2(rig.velocity.x, velocidadePulo);
            //anim.SetBool("Pulando", true);
            pulos--;
        }
    }
    private bool isGrounded()
    {
        //Criar o meu raycast         //Pegando os limites do meu colisor
        bool chao = Physics2D.Raycast(boxColl.bounds.center, Vector2.down, 0.5f, layerLevel);
        return chao;
    }
    private void AbrindoPorta()
    {
        if (minhaPorta != null && !morto)
        {
            //Checando se a porta tem um destino
            if (minhaPorta.tenhoDestino())
            {
                if (Input.GetKeyUp(KeyCode.H))
                {
                    minhaPorta.Abrindo();
                    Invoke("Entrando", 1f);
                    morto = true;
                    rig.velocity = Vector2.zero;
                    anim.SetBool("Correndo", false);
                }
            }
        }
    }

    private void Entrando()
    {
        anim.SetTrigger("Entrando");
    }
    private void Restart()
    {
        if (restartPlayer == true)
        {
            rig.transform.position = new Vector2(inicialPos.transform.position.x, inicialPos.transform.position.y);
            restartPlayer = false;
        }
        if (restartPlayer1 == true)
        {
            rig.transform.position = new Vector2(inicialPos1.transform.position.x, inicialPos1.transform.position.y);
            restartPlayer1 = false;
        }
    }
    void Ataque() 
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if(esperaAtaque <= 0) 
            {
                anim.SetTrigger("Attack");
                esperaAtaque = 1f;
                Collider2D[] hit = Physics2D.OverlapCircleAll(ataque.position, alcanceAtaque, layerEnemy);
                foreach (Collider2D inimigo in hit)
                {
                    inimigo.GetComponent<Inimigo1Controller>().RecebendoDano(1);
                    //inimigo.GetComponent<InimigoKing>().RecebendoDano1(1);
                    Debug.Log("Hit" + inimigo.name);
                }
            }
        }
        else
        {
            esperaAtaque -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão")) 
        {
            //anim.SetBool("Pulando", false);
            anim.SetBool("Chão", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão")) 
        {
            anim.SetBool("Pulando", true);
        }
    }
    //Raycast de colisão no chão

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10) 
        {
            if(transform.position.y > collision.transform.position.y) 
            {
                rig.velocity = new Vector2(rig.velocity.x, velocidadePulo);
                //collision.GetComponentInParent<Inimigo1Controller>().morrendo();
                //collision.GetComponent<BoxCollider2D>().enabled = false;
                collision.GetComponentInParent<Inimigo1Controller>().RecebendoDano(1);
            }
            else 
            {
                if (!morto) 
                {
                    if (invencivel <= 0)
                    {
                        vida--;
                        anim.SetTrigger("Hit");
                        //Informando ao game manager que a minha vida mudou
                        gm.SetVida(vida);
                        invencivel = 2f;
                        //anim.SetInteger("Vida", vida);
                        ////Informando ao game manager que ele tem que ajustar a vida
                        gm.AjustaVida();

                    }
                }
            }
        }

        if (collision.CompareTag("Portal") == true)
        {
            restartPlayer = true;
        }
        if (collision.CompareTag("Portal1") == true)
        {
            restartPlayer1 = true;
        }
        if (collision.gameObject.CompareTag("Porta")) 
        {
            minhaPorta = collision.GetComponent<PortaController>();
        }
        if (collision.gameObject.CompareTag("Bomba")) 
        {
            vida = 0;
            gm.SetVida(vida);
            ////Informando ao game manager que ele tem que ajustar a vida
            gm.AjustaVida();
            anim.SetTrigger("Hit");
            anim.SetTrigger("vida");
        }
        if (collision.gameObject.CompareTag("Bomba1")) 
        {
            rig.velocity = new Vector2(0f, rig.velocity.y);
            anim.SetBool("Correndo", false);
            morto = true;
        }
        if (collision.gameObject.CompareTag("Box")) 
        {
            if (transform.position.y > collision.transform.position.y)
            {
                rig.velocity = new Vector2(rig.velocity.x, velocidadePulo);
            }
        }
        if (collision.gameObject.CompareTag("Vida")) 
        {
            if(vida < 3) 
            {
                vida++;
                gm.SetVida(vida);
                gm.AjustaVida();
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Diamante")) 
        {
            gm.Ganha();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Diamante1"))
        {
            gm.Ganha1();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Aviso")) 
        {
            gm.Danger();
            Instantiate(barra, tl.position, transform.rotation);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Porta"))
        {
            minhaPorta = null;
        }
    }
    void PauseScreen()
    {
        if (IsPaused)
        {
            IsPaused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {
            IsPaused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
