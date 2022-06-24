using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ParticleMove : MonoBehaviour
{
    [SerializeField] GameObject target, obj;
    [SerializeField]float speed = 9f;
    [SerializeField]float lifeTime = 4f;
    public bool player;
    GameObject playerObject;
    public float manaCost = 0.05f;
    public bool invisible = false;
    public bool invincibility = false;
    public bool strenght = false;
    public bool heal = false;
    WaitForSeconds wait = new WaitForSeconds(5.7f);
    WaitForSeconds wait2 = new WaitForSeconds(3f);
    WaitForSeconds wait3 = new WaitForSeconds(10f);
    WaitForSeconds wait4 = new WaitForSeconds(0.45f);
    [SerializeField] float damageAmount = 0.3f;
    ZombieMovement zombie;
    public bool hurricane;
    public bool groundEffects;
    [SerializeField] BoxCollider col;
    private void Start()
    {
        Save.costSpell = manaCost;
        StartCoroutine(enumerator());
        Save.manaAmount -= (Save.costSpell-(Save.manaPower/25));
        if(invisible)
        {
            Save.invisible = true;
            StartCoroutine(OnBecomeVisible());
        }
        if (invincibility)
        {
            Save.invincibility = true;
            StartCoroutine(OnBecomeVulnerable());
        }
        if (strenght)
        {
            Save.Strength = true;
            StartCoroutine(OnBecomeStrenght());
        }
        if (heal)
        {
            playerObject.GetComponent<Player>().Heal(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(playerObject == null)
        {
            playerObject = PlayerDisplay.instance.players[Save.pchar];
        }
        if (target != null &&  groundEffects == false)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target.transform.position + Vector3.up, speed * Time.deltaTime);
        }
        else if (target != null && groundEffects)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
        if (player)
        {
            target = playerObject;
            Destroy(gameObject, lifeTime);
        }
        else
        {
            Destroy(obj, lifeTime);
        }
    }
    IEnumerator enumerator()
    {
        yield return wait4;
        if (player == false)
        {
            target = Player.enemy;
        }
        else
        {
            playerObject = PlayerDisplay.instance.players[Save.pchar];
        }
    }
    IEnumerator OnBecomeVisible()
    {
        yield return wait;
        Save.invisible = false;
    }
    IEnumerator OnBecomeVulnerable()
    {
        yield return wait2;
        Save.invincibility = false;
    }
    IEnumerator OnBecomeStrenght()
    {
        yield return wait3;
        Save.Strength = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            if (hurricane == false)
            {
                zombie = other.GetComponent<ZombieMovement>();
                if (zombie != null && other.gameObject != zombie)
                {
                    zombie = null;
                    zombie = other.GetComponent<ZombieMovement>();
                }
                zombie.Dmg(damageAmount * (Save.playerLevel * 1.5f));
                zombie.AtirouEmMim();
                zombie.anim.SetTrigger("hit");
            }
            else
            {
                zombie = other.GetComponent<ZombieMovement>();
                if (zombie != null && other.gameObject != zombie)
                {
                    zombie = null;
                    zombie = other.GetComponent<ZombieMovement>();
                }
                zombie.Dmg(0.1f);
                StartCoroutine(Tornado());
                zombie.TornadoInMe();
                
            }
        }

    }
    IEnumerator Tornado()
    {
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }
}
