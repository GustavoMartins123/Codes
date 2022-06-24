using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] float speed = 10;
    [SerializeField] GameObject hit;

    public enum Targets 
    {
        Enemy,
        Player
    }
    public Targets targets;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed) ;
    }

    public void SetDamage(int amount) 
    {
        dmg = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targets == Targets.Player) 
        {
            if (other.gameObject.layer == 6)
            {
                other.GetComponent<Player>().Damage(dmg);
                Instantiate(hit, transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }
        if(targets == Targets.Enemy) 
        {
            if (other.gameObject.layer == 8)
            {
                other.GetComponent<Enemy>().Damage(dmg);
                    if (other.GetComponent<Enemy>().health < 2 && gameObject.tag!= "Torpedo")
                    {
                        other.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                Instantiate(hit, transform.position, other.transform.rotation);
                Destroy(gameObject);

            }
        }

        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
        
    }
}
