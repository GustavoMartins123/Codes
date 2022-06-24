using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    bool isDragged;
    Vector3 screenPoint;
    Vector3 offSet;

    float bullet;
    [SerializeField] Transform[] bulletSpawn;
    [SerializeField]float fireRate = 0.5f;
    int bulletLvl = 2;
    int bulletDmg = 1;
    [SerializeField] GameObject bulletObj;
    [SerializeField] int health;
    [SerializeField] GameObject explosion;
    Vector3 posStart;
    bool isDead;
    [SerializeField] AudioClip fire, expl;
    private void Start()
    {
        posStart = transform.position;
    }

    void Update()
    {
        if(isDragged && Time.time > bullet && !isDead) 
        {
            bullet = Time.time + fireRate;
            for (int i = 0; i <bulletLvl ; i++)
            {
                GameObject @object= Instantiate(bulletObj, bulletSpawn[i].position, bulletSpawn[i].rotation);
                @object.GetComponent<Bullet>().SetDamage(bulletDmg);
                AudioSource.PlayClipAtPoint(fire, bulletSpawn[i].position);
            }
        }
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offSet = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        if (!isDead)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offSet;
            isDragged = true;
            transform.position = curPosition;
            float meux = Mathf.Clamp(transform.position.x, -5.10f, 5.10f);
            float meuz = Mathf.Clamp(transform.position.z, -7, 8.60f);
            transform.position = new Vector3(meux, transform.position.y, meuz);
        }

    }

    private void OnMouseUp()
    {
        isDragged = false;
    }

    public void Damage(int amount) 
    {
        health -= amount;
        if(health <= 0) 
        {
            //add song
            //add particles
            if(explosion!= null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            //Destroy(gameObject);
            AudioSource.PlayClipAtPoint(expl, transform.position);
            StartCoroutine(Reborn());

        }
    }

    IEnumerator Reborn() 
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        isDead = true;
        transform.position = posStart;
        yield return new WaitForSeconds(2.3f);
       
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GameManager.instance.DecreaseLife();
        health = 5;
        isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8) 
        {
            Damage(5);
        }
    }
}
