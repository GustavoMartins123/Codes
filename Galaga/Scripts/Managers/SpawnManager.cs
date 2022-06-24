using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float enemyTime;
    [SerializeField] float waveInterval;
    [SerializeField] int currentWave;
    int flyId = 0;
    int midId = 0;
    int bossId = 0;
    [SerializeField] GameObject[] smallEnemy;
    [SerializeField] GameObject[] midEnemy;
    [SerializeField] GameObject[] BossEnemy;

    public Formation smallShipsFormation;
    public Formation midShipsFormation;
    public Formation bossShipsFormation;

    bool spawnComplete;

    [Serializable]
    public class Wave 
    {
        public int flyAmount;
        public int midAmount;
        public int BossAmount;
        public GameObject[] patchPreFab;


    }
    public List<Wave> waves = new List<Wave>();
    List<Patch> activePatchList = new List<Patch>();

    [HideInInspector]public List<GameObject> spawnedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartSpawn",3f);
    }

    IEnumerator spawnWaves() 
    {
        while(currentWave < waves.Count) 
        {
            if(currentWave == waves.Count-1)
            {
                spawnComplete = true;
            }
            for (int i = 0; i < waves[currentWave].patchPreFab.Length; i++)
            {
                int y = UnityEngine.Random.Range(0, 3);
                GameObject @object = Instantiate(waves[currentWave].patchPreFab[i], new Vector3(0,0,2.5f), Quaternion.identity) as GameObject;
                Patch newPatch = @object.GetComponent<Patch>();
                activePatchList.Add(newPatch);
            }
            for (int i = 0; i < waves[currentWave].flyAmount; i++)
            {
                int go = UnityEngine.Random.Range(0, 3);
                GameObject newFly = Instantiate(smallEnemy[go], transform.position, Quaternion.identity) as GameObject;
                Enemy enemy = newFly.GetComponent<Enemy>();
                enemy.Spawn(activePatchList[PathPingPong()], flyId, smallShipsFormation );
                flyId++;
                spawnedObjects.Add(newFly);
                yield return new WaitForSeconds(enemyTime);
            }

            for (int i = 0; i < waves[currentWave].midAmount; i++)
            {
                int go = UnityEngine.Random.Range(0, 3);
                GameObject newFly = Instantiate(midEnemy[go], transform.position, Quaternion.identity) as GameObject;
                Enemy enemy = newFly.GetComponent<Enemy>();
                enemy.Spawn(activePatchList[PathPingPong()], midId, midShipsFormation);
                midId++;
                spawnedObjects.Add(newFly);
                yield return new WaitForSeconds(enemyTime);
            }

            for (int i = 0; i < waves[currentWave].BossAmount; i++)
            {
                int go = UnityEngine.Random.Range(0, 3);
                GameObject newFly = Instantiate(BossEnemy[go], transform.position, Quaternion.identity) as GameObject;
                Enemy enemy = newFly.GetComponent<Enemy>();
                enemy.Spawn(activePatchList[PathPingPong()], bossId, bossShipsFormation);
                bossId++;
                spawnedObjects.Add(newFly);
                yield return new WaitForSeconds(enemyTime);
            }
            yield return new WaitForSeconds(waveInterval);
            currentWave++;
            foreach(Patch patch in activePatchList) 
            {
                Destroy(patch.gameObject);
            }
            activePatchList.Clear();
        }

        Invoke("CheckEnemyState", 1f);
    }

    void CheckEnemyState() 
    {
        bool information = false;
        for (int i = spawnedObjects.Count-1; i>=0 ; i--)
        {
            if(spawnedObjects[i].GetComponent<Enemy>().enemy != Enemy.EnemyState.Idle) 
            {
                information = false;
                Invoke("CheckEnemyState", 1f);
                break;
            }
        }
        information = true;

        if (information) 
        {
            StartCoroutine(smallShipsFormation.ActiveSpread());
            StartCoroutine(midShipsFormation.ActiveSpread());
            StartCoroutine(bossShipsFormation.ActiveSpread());
            CancelInvoke("CheckEnemyState");
        }
    }

    void StartSpawn() 
    {
        StartCoroutine(spawnWaves());
        CancelInvoke("StartSpawn");
    }

    int PathPingPong() 
    {
        return (flyId + bossId + midId) % activePatchList.Count;
    }

    private void OnValidate()
    {
        int currentSmallAmount = 0;
        for (int i = 0; i < waves.Count; i++)
        {
            currentSmallAmount += waves[i].flyAmount;
            
        }
        if(currentSmallAmount > 16) 
        {
            Debug.LogError("<color=red>Error!! Fix this Bro Lmao, current have: </color> " + currentSmallAmount + " <color=green>/16 is the Maximum</color> WTF!!!!!!");
        }



        int currentMidAmount = 0;
        for (int i = 0; i < waves.Count; i++)
        {
            currentMidAmount += waves[i].midAmount;

        }
        if (currentMidAmount > 16)
        {
            Debug.LogError("<color=red>Error!! Fix this Bro Lmao, current have: </color> " + currentMidAmount + " <color=green>/16 is the Maximum</color> WTF!!!!!!");
        }


        int currentBossAmount = 0;
        for (int i = 0; i < waves.Count; i++)
        {
            currentBossAmount += waves[i].BossAmount;

        }
        if (currentBossAmount > 4)
        {
            Debug.LogError("<color=red>Error!! Fix this Bro Lmao, current have: </color> " + currentBossAmount + " <color=green>/4 is the Maximum</color> WTF!!!!!!");
        }
    }

    void ReportToGameManager() 
    {
        if(spawnedObjects.Count == 0 && spawnComplete) 
        {
            StartCoroutine(win());
        }
    }
    public void UpdateSpawnedEnemies(GameObject game)
    {
        spawnedObjects.Remove(game);
        ReportToGameManager();
    }
    IEnumerator win()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.WinCondition();
    }
}
