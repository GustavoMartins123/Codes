using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum States
{
    OnGround,
    Walking
}
public class ZombieMovement : MonoBehaviour
{
    public States states;
    [SerializeField]float tempoP = 20f;
    WaitForSeconds tempo;
    public Transform[] waypoints;
    int index;
    public NavMeshAgent agent;
    public Animator anim;
    public GameObject pos;
    //player
    public GameObject player;
    bool ataca;
    [SerializeField] float distAtaque;
    //Fov
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    [SerializeField] float speed;
    Transform ultPos;
    public bool morto;
    public float health = 1;
    //event crying
    public bool crying;
    public bool inscrito = false;//Delegate
    WaitForSeconds seconds = new WaitForSeconds(.4f);
    WaitForSeconds fovRoutine = new WaitForSeconds(0.2f);
    [SerializeField]Collider col;
    public Image healthBar;
    [SerializeField]GameObject barHealth;
    public Collider colPr, torso;
    bool playerPassToMe = false;
    float rotateSpeed = 5f;
    [SerializeField] GameObject coin;
    IamTarget iamTarget;
    Camera cam;
    float xp;
    public float armorValue;
    SkinnedMeshRenderer rendererSkin;
    public bool iamBoss, iamDragon, iamSpider;
    float time;
    public float[] determinedTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        tempo = new WaitForSeconds(tempoP);
        anim = GetComponent<Animator>();
        if (iamBoss == false)
        {
            distAtaque = agent.stoppingDistance;
        }
        index = Random.Range(0, waypoints.Length);
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pos.transform.GetChild(i);
        }
        if (states == States.Walking)
        {
            agent.isStopped = false;
            StartCoroutine(chamaPatrulha());
        }
        if (states == States.OnGround)
        {
            agent.isStopped = true;
        }
        StartCoroutine(FovRoutine());
        iamTarget = GetComponent<IamTarget>();
        rendererSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        if (iamBoss)
        {
            xp = Random.Range(200, 400);
        }
        else
        {
            xp = Random.Range(10, 50);
        }
    }
    IEnumerator FovRoutine() 
    {
        WaitForSeconds wait = fovRoutine;
        while (morto == false) 
        {
            yield return wait;
            FovCheck();
        }
    }
    void FovCheck() 
    {
        Collider[] range = Physics.OverlapSphere(transform.position, radius, targetMask);
        if(range.Length != 0) 
        {
            Transform target = range[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, direction)< angle / 2) 
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, direction, distance, obstructionMask)) 
                {
                    canSeePlayer = true;
                    playerPassToMe = true;
                }
                else 
                {
                    canSeePlayer = false;
                }
            }
            else 
            {
                canSeePlayer = false;
                
            }
        }
        else if (canSeePlayer) 
        {
            
            if (states == States.Walking)
            {
                canSeePlayer = false;
            }
            ultPos = player.transform;
            barHealth.SetActive(true);
            playerPassToMe = true;

        }
            if (canSeePlayer == false && anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 16f)
                {
                    agent.destination = player.transform.position;
                }
                else
                {
                    agent.destination = ultPos.position;
                }
                anim.SetBool("ataque", false);
                ataca = false;
            }
            if (canSeePlayer == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && playerPassToMe && Vector3.Distance(transform.position, player.transform.position) < 30f)
            {
                playerPassToMe = false;
                ataca = false;
                agent.destination = player.transform.position;
            }
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (player == null)
            {
                player = PlayerDisplay.instance.players[Save.pchar].gameObject;
            }
            if (states == States.Walking && morto == false)
            {
                if (canSeePlayer)
                {
                    PegaPlayer();
                }   
                Ataque();
                anim.SetFloat("X", agent.velocity.sqrMagnitude, 0.06f, Time.deltaTime);
            }
            else if (states == States.OnGround && canSeePlayer && morto == false)
            {
                anim.SetFloat("X", agent.velocity.sqrMagnitude);
                StartCoroutine(EstouTeVendo());
                Ataque();
            }
            if (barHealth.activeSelf)
            {
                barHealth.transform.LookAt(cam.transform.position);
            }
            /*if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && ataca && agent.speed ==0)
            {
                StartCoroutine(EstouTeVendo());
            }*/

        }

    }

    IEnumerator EstouTeVendo()
    {
        agent.isStopped = false;
        ataca = false;
        // anim.SetBool("player", true);
        yield return seconds;
        PegaPlayer();
    }

    public IEnumerator chamaPatrulha() 
    {
        while (morto == false) 
        {
            yield return tempo;
            Patrol();
            if (barHealth.activeSelf)
            {
                barHealth.SetActive(false);
            }

        }
    }

    private void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
        agent.destination = waypoints[index].position;
    }
    void PegaPlayer() 
    {
        agent.destination = player.transform.position;
        barHealth.SetActive(true);
    }

    public void AtirouEmMim()
    {
        if(morto == false)
        {
            PegaPlayer();
        }
    }
    void Ataque() 
    {
        if (iamBoss && canSeePlayer)
        {
            time += Time.deltaTime;
            if (time > determinedTime[0] && Vector3.Distance(transform.position, player.transform.position) < distAtaque / 2 && iamDragon == false)
            {
                time = 0;
                anim.SetBool("Far", true);
                anim.SetBool("ataque", true);
                ataca = true;
            }
            else if (time > determinedTime[0] && Vector3.Distance(transform.position, player.transform.position) < 40 && iamDragon)
            {
                time = 0;
                anim.SetBool("Far", true);
                anim.SetBool("ataque", true);
                ataca = true;
            }
            else if (time > determinedTime[1] && Vector3.Distance(transform.position, player.transform.position) < distAtaque / 3)
            {
                time = 0;
                anim.SetBool("ataque", true);
                anim.SetBool("Far", false);
                ataca = true;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > (distAtaque / 2) && iamDragon == false)
            {
                agent.destination = player.transform.position;
            }
            else
            {
                anim.SetBool("ataque", false);
                anim.SetBool("Far", false);
            }
            Vector3 pos = (player.transform.position - transform.position).normalized;
            Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
        }
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < distAtaque && canSeePlayer && iamBoss ==false)
        {
            anim.SetBool("ataque", true);
            playerPassToMe = true;
            ataca = true;
            Vector3 pos = (player.transform.position - transform.position).normalized;
            Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > distAtaque && canSeePlayer && morto == false && iamBoss == false) 
        {
            anim.SetBool("ataque", false);
            //ataca = false;
        }
        if (ataca == true) 
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
        else 
        {
            agent.speed = speed;
            agent.isStopped = false;
        }
    }
    void ColEnable(int i)
    {
        if(i == 0)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
    void AnulaAtaque() 
    {
        ataca = false;
    }

    void cryingFalse()
    {
        crying = false;
    }
    public void Dmg(float amount)
    {
        float result = amount - armorValue;
        if(result > 0)
        {
            result = 0;
        }
        health -= result;
        if(health > 1)
        {
            health = 1;
        }
        if (health <= 0)
        {
            
            Death();
        }
        healthBar.fillAmount = health;
    }
    public void TornadoInMe()
    {
        morto = true;
        agent.speed = 0;
        agent.isStopped = true;
        transform.LookAt(player.transform);
        StartCoroutine(Tornado());
    }
    IEnumerator Tornado()
    {
        yield return new WaitForSeconds(6f);
        if(health > 0)
        {
            morto = false;
            agent.speed = speed;
            agent.isStopped = false;
            AtirouEmMim();
        }
    }
    private void Death()
    {
        morto = true;
        StopAllCoroutines();
        colPr.enabled = false;
        col.enabled = false;
        player = null;
        agent.speed = 0;
        agent.isStopped = true;
        anim.SetTrigger("death");
        if (iamSpider)
        {
            
            if (Open_Inventory.instance.objectivesReceived[1].interactable)
            {
                Open_Inventory.instance.objectives[4] = true;
            }
            col.enabled = false;
            torso.enabled = false;
            if(Open_Inventory.instance.messages[4].text == "Blank")
            {
                Open_Inventory.instance.messages[4].color = Color.clear;
            }
            Open_Inventory.instance.spider = true;
        }
        if (iamDragon)
        {
            
            if (Open_Inventory.instance.objectivesReceived[0].interactable)
            {
                Open_Inventory.instance.objectives[3] = true;
            }
            col.enabled = false;
            if(Open_Inventory.instance.messages[3].text == "Blank")
            {
                Open_Inventory.instance.messages[3].color = Color.clear;
            }
            Open_Inventory.instance.dragon = true;
        }
        if (Player.enemy == gameObject)
        {
            Player.enemy = null;
        }
        if(iamBoss == false)
        {
            iamTarget.outlineOn = false;
        }
        Save.xpLvl += xp;
        Instantiate(coin, transform.position, coin.transform.rotation);
        StartCoroutine(Reborn());
    }
    IEnumerator Reborn()
    {
        if (iamBoss == false)
        {
            yield return new WaitForSeconds(6f);
            rendererSkin.enabled = false;
            yield return new WaitForSeconds(10f);
            morto = false;
            StartCoroutine(chamaPatrulha());
            health = 1f;
            healthBar.fillAmount = health;
            colPr.enabled = true;
            player = PlayerDisplay.instance.players[Save.pchar];
            anim.Play("Idle");
            rendererSkin.enabled = true;
            agent.isStopped = true;
            yield return new WaitForSeconds(6f);
            agent.isStopped = false;
            xp = Random.Range(10, 50);
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, dist);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, (distAtaque/3)*2);
    }
    void OnAnimatorIK(int layerIndex)
    {
        if (canSeePlayer)
        {
            anim.SetLookAtWeight(0.6f);
            anim.SetLookAtPosition(player.transform.position);
        }
    }

}
