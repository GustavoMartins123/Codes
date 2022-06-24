using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControll : MonoBehaviour
{
    public float vida = 100f;
    public GameObject tronco;
    public bool quebrou;
    [SerializeField] BoxCollider box;
    Building building;
    // Start is called before the first frame update
    void Start()
    {
        building = FindObjectOfType<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0 && quebrou == false) 
        {
            transform.GetComponent<Collider>().enabled = true;
            tronco.GetComponent<Collider>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<Rigidbody>().AddForce(tronco.transform.forward * 30000 * Time.deltaTime);
            box.enabled = false;
            quebrou = true;
            if(quebrou == true) 
            {
                gameObject.layer = 8;
                gameObject.tag = "GameController";
                building.Wood += 25;
                Destroy(gameObject, 8f);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 10) 
        {
            vida -= 20;
            axe.t++;
        }
    }
}
