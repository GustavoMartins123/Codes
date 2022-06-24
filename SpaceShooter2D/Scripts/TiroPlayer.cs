using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    [SerializeField] private float velocidade = 15f;
    [SerializeField] private Animator anim;
    private Rigidbody2D rig;
    [SerializeField] private GameObject Hit;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tiroPlayer();
    }
    public void tiroPlayer()
    {
        rig.velocity = new Vector2(0f, velocidade);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            collision.GetComponent<Inimigo01>().LevarDano(1);
            Instantiate(Hit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("ParedePlayer")) 
        {
            Destroy(gameObject);
        }
    }
}
