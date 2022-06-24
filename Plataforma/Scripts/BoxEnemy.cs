using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private Animator anim;
    [SerializeField] private GameObject diamante;
    [SerializeField] private GameObject dialogo;
    [SerializeField] private GameObject dialogo2;
    [SerializeField] private Transform tl;
    [SerializeField] private float timer = 2f;
    [SerializeField] private LayerMask layerPlayer;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isGrounded()) 
        {
            anim.SetBool("Olhando", true);
            if (timer <= 0)
            {
                Dialogo();
                timer = 4f;
            }
        }
        else 
        {
            anim.SetBool("Olhando", false);
            timer -= Time.deltaTime;
        }
    }

    private void Dialogo()
    {
         Instantiate(dialogo, tl.position, transform.rotation);
    }

    private bool isGrounded()
    {
        //Criar o meu raycast         //Pegando os limites do meu colisor
        bool player = Physics2D.Raycast(boxCol.bounds.center, Vector2.left * 3, 5f, layerPlayer);
        Debug.DrawRay(boxCol.bounds.center, Vector2.left * 3, Color.red);
        return player;
    }
    private void Destruindo()
    {
        Instantiate(diamante, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                Bati();
            }
        }
    }

    public void Bati()
    {
        anim.SetTrigger("Hit");
    }
}
