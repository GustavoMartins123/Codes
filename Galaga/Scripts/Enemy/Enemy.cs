using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ID 
{
    small,
    mid,
    boss
}

public class Enemy : MonoBehaviour
{
    public Patch follow;

    public int currentWayPointsID = 0;
    public float speed = 2;
    public float reachDistance = 0.4f;//Distance to next Point
    public float rotationSpeed = 5f;
    float distance;//current distance to the next point
    public bool useBezier;
    public enum EnemyState 
    {
        Fly,
        Formation,
        Idle,
        Dive
    }
    public EnemyState enemy;
    public ID type;
    public int enemyId;
    public Formation formation;

    public int health = 3;

    [SerializeField] GameObject bullet;
    float delay;
    float fireRate = 1.5f;
    Transform targetPlayer;
    [SerializeField]Transform spawnPoint;
    int bulletDmg = 1;


    [SerializeField] int scoreIformation, notScoreInformation;

    [SerializeField] TrailRenderer[] trailRenderers;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip expl;
    private void Awake()
    {
        spawnPoint = GetComponentInChildren<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        /*if(type == ID.small) 
        {
            formation = GameObject.Find("Formation").GetComponent<Formation>();
        }
        else if(type == ID.mid) 
        {
            formation = GameObject.Find("MidFormation").GetComponent<Formation>();
        }
        else 
        {
            formation = GameObject.Find("BossFormation").GetComponent<Formation>();
        }
        follow = FindObjectOfType<Patch>();*/
        targetPlayer = GameObject.Find("PlayerShip").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemy)
        {
            case EnemyState.Fly:
                TrailActive(true);
                MoveOnThePatch(follow);
                break;
            case EnemyState.Formation:
                //TrailActive(true);
                MoveToFormation();
                break;
            case EnemyState.Idle:
                TrailActive(false);
                break;
            case EnemyState.Dive:
                TrailActive(true);
                MoveOnThePatch(follow);
                SpawnBullet();
                break;
        }
            
    }
    void MoveToFormation() 
    {
        transform.position = Vector3.MoveTowards(transform.position, formation.GetVector(enemyId), speed * Time.deltaTime);

        var direction = formation.GetVector(enemyId) - transform.position;
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            direction = direction.normalized;
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, formation.GetVector(enemyId)) <= 0.001f) 
        {
            transform.SetParent(formation.gameObject.transform);
            transform.eulerAngles = Vector3.zero;

            formation.enemyFormations.Add(new Formation.EnemyFormation(enemyId, transform.localPosition.x, transform.localPosition.z, this.gameObject));

            enemy = EnemyState.Idle;
        }
    }

    void MoveOnThePatch(Patch patch) 
    {
        if (useBezier) 
        {
            distance = Vector3.Distance(patch.bezierObj[currentWayPointsID], transform.position);
            transform.position = Vector3.MoveTowards(transform.position, patch.bezierObj[currentWayPointsID], speed * Time.deltaTime);
            var direction = patch.bezierObj[currentWayPointsID] - transform.position;
            if(direction != Vector3.zero) 
            {
                direction.y = 0;
                direction = direction.normalized;
                var rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation,rotation, rotationSpeed * Time.deltaTime);
            }
        }
        else 
        {
            distance = Vector3.Distance(patch.patchObj[currentWayPointsID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, patch.patchObj[currentWayPointsID].position, speed * Time.deltaTime);
            var direction = patch.patchObj[currentWayPointsID].position - transform.position;
            if (direction != Vector3.zero)
            {
                direction.y = 0;
                direction = direction.normalized;
                var rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }
        

        if (useBezier) 
        {
            if (distance <= reachDistance)
            {
                currentWayPointsID++;
            }
            if (currentWayPointsID >= patch.bezierObj.Count)
            {
                currentWayPointsID = 0;
                if(enemy == EnemyState.Dive) 
                {
                    TrailActive(false);
                    transform.position = GameObject.Find("SpawnManager").transform.position;
                    Destroy(follow.gameObject);

                }
                enemy= EnemyState.Formation;
            }
        }
        else 
        {
            if(distance <= reachDistance) 
            {
                currentWayPointsID++;
            }
            if(currentWayPointsID >= patch.patchObj.Count) 
            {
                currentWayPointsID = 0;
                if (enemy == EnemyState.Dive)
                {
                    TrailActive(false);
                    transform.position = GameObject.Find("SpawnManager").transform.position;
                    Destroy(follow.gameObject);
                }
                enemy = EnemyState.Fly;
            }
        }
    }

    public void Spawn(Patch patch, int id, Formation formation) 
    {
        follow = patch;
        enemyId = id;
        this.formation = formation;
    }
    public void DiveSetup(Patch patch) 
    {
        follow = patch;
        transform.SetParent(transform.parent.parent);
        enemy = EnemyState.Dive;
    }
    public void Damage(int amount) 
    {
        health -= amount;
        if(health<= 0) 
        {
            //Add Sound
            //Add Particle
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            if (enemy == EnemyState.Idle) 
            {
                GameManager.instance.AddScore(scoreIformation);
            }
            else
            {
                GameManager.instance.AddScore(notScoreInformation);
            }

            for (int i = 0; i < formation.enemyFormations.Count; i++)
            {
                if(formation.enemyFormations[i].index == enemyId)
                {
                    formation.enemyFormations.Remove(formation.enemyFormations[i]);
                }
                    
            }
            SpawnManager sp = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            /*for (int i = 0; i < sp.spawnedObjects.Count; i++)
            {
                sp.spawnedObjects.Remove(this.gameObject);
            }*/
            sp.UpdateSpawnedEnemies(this.gameObject);
            AudioSource.PlayClipAtPoint(expl, transform.position);
            //GameManager.instance.ReduceEnemy();
            Destroy(gameObject);
        }
    }


    void SpawnBullet() 
    {
        delay += Time.deltaTime;
        if(delay >= fireRate && bullet!= null && spawnPoint!= null) 
        {
            spawnPoint.LookAt(targetPlayer);
            GameObject @object = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
            @object.GetComponent<Bullet>().SetDamage(bulletDmg);
            delay = 0;
        }
    }

    void TrailActive(bool on) 
    {
        foreach (TrailRenderer trail in trailRenderers)
        {
            trail.enabled = on;
        }
    }
}
