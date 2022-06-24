using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human_Movement : MonoBehaviour
{
    public float tempoP = 20f;
    WaitForSeconds tempo;
    public Transform[] waypoints;
    int index;
    public NavMeshAgent agent;
    Animator anim;
    public GameObject pos;
    //player
    public GameObject ObjectToHit;
    int t = 0;
    bool ataca;
    [SerializeField] float dist = 5;
    [SerializeField] float distAtaque;
    //Fov
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    [SerializeField]bool vivo = false;
    public bool inscrito = false;
    [SerializeField] GameObject Axe;
    float tempoAxe = 2.2f;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.FindGameObjectWithTag("pos");
        tempo = new WaitForSeconds(tempoP);
        agent = GetComponent<NavMeshAgent>();
        index = Random.Range(0, waypoints.Length);
        StartCoroutine(chamaPatrulha());
        anim = GetComponent<Animator>();
        //distAtaque = agent.stoppingDistance;
        ObjectToHit = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pos.transform.GetChild(i);
        }
        if (inscrito) 
        {
            Evento_Arvore.arvore +=Arvore ;
        }
    }
    void Update()
    {
        anim.SetFloat("y", agent.velocity.sqrMagnitude, 0.06f, Time.deltaTime);
        if(vivo == true)
        {
            StopAllCoroutines();
        }
            
        Ataque();
        Debug.Log(Vector3.Distance(transform.position, ObjectToHit.transform.position));
        if (ataca == true)
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
        else
        {
            agent.speed = 2f;
            agent.isStopped = false;
        }

    }
    IEnumerator chamaPatrulha()
    {
        while (vivo == false)
        {
            yield return tempo;
            Patrol();
        }

    }

    private void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
        agent.destination = waypoints[index].position;
    }


    void Ataque()
    {
        if (ObjectToHit != null && Vector3.Distance(transform.position, ObjectToHit.transform.position) < distAtaque)
        {
            anim.SetBool("ataque", true);
            ataca = true;
            Axe.SetActive(true);
            if (axe.t == 5)
            {
                ObjectToHit = null;
                anim.SetBool("ataque", false);
                vivo = false;
                StartCoroutine(chamaPatrulha());
                ataca = false;
                Axe.SetActive(false);
                axe.t = 0;
            }
        }
        else if (Vector3.Distance(transform.position, ObjectToHit.transform.position) > distAtaque)
        {
            anim.SetBool("ataque", false);
        }
        if (ataca == true)
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
        else
        {
            agent.speed = 2f;
            agent.isStopped = false;
        }
    }
    public void Arvore() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 350) && hit.collider.tag == "arvore")
            {
                vivo = true;
                agent.SetDestination(hit.point);
                Axe.SetActive(false);
                anim.SetBool("ataque", false);
                ObjectToHit = hit.transform.gameObject;
                ataca = false;
            }
        }

    }
    void AnulaAtaque()
    {
        ataca = false;
    }
}
