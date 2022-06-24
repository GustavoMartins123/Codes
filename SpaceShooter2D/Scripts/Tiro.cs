using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private Rigidbody2D rig;
    [SerializeField] private GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Parede")) 
        {
            Destroy(gameObject);
            Instantiate(hit, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().LevarDanoPlayer(1);
            Instantiate(hit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
