using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoKing : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private Animator anim;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private GameObject diamante;
    private Rigidbody2D rig;

    private int totalPulos = 1;
    [SerializeField] private int pulos = 1;
    [SerializeField] private float velocidadePulo = 8f;
    private Transform posicaoDojogador;
    private float velocidade = 1f;
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        posicaoDojogador = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        anim.SetBool("Pulando", isGrounded());
        if (isGrounded()) 
        {
            anim.SetBool("Ataque", true);
            Pulando();
            pulos = totalPulos;
            if (posicaoDojogador.gameObject != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, posicaoDojogador.position, velocidade * Time.deltaTime);
            }
        }
        else 
        {
            anim.SetBool("Ataque", false);
        }
    }
    private bool isGrounded()
    {
        //Criar o meu raycast         //Pegando os limites do meu colisor
        bool player = Physics2D.Raycast(boxCol.bounds.center, Vector2.left, 0.5f, layerPlayer);
        return player;
    }
    private void Pulando()
    {
        anim.SetFloat("Jump", rig.velocity.y);
        if (pulos > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, velocidadePulo);
            //anim.SetBool("Pulando", true);
            pulos--;
        }
    }
    private void OnDestroy()
    {
        Instantiate(diamante, transform.position, transform.rotation);
    }
}
