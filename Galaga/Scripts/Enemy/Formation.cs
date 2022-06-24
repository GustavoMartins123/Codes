using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Formation : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 2;

    public float gridOffSetX = 1f;
    public float gridOffSetZ = 1f;

    public int div = 4;

    public List<Vector3> gridList = new List<Vector3>();

    public float maxMoveX = 5f;

    float currentPosX;
    Vector3 startPos;

    public float speed = 1f;
    int dir = -1;

    bool canSpread;
    bool spreadStart;
    float spreadAmount = 1;
    float currentSpread;
    float spreadSpeed = 0.5f;
    int spreadDir = 1;

    bool canDive;
    public List<GameObject> divePatchList = new List<GameObject>();

    [HideInInspector]public List<EnemyFormation> enemyFormations = new List<EnemyFormation>();

    [System.Serializable]
    public class EnemyFormation 
    {
        public int index;
        public float xPos;
        public float zPos;
        public GameObject objectEnemy;
        public Vector3 goal;
        public Vector3 start;
        public EnemyFormation(int _index, float _xPos, float _zPos, GameObject _objectEnemy) 
        {
            index = _index;
            xPos = _xPos;
            zPos = _zPos;
            objectEnemy = _objectEnemy;

            start = new Vector3(_xPos, 0, _zPos);
            goal = new Vector3(_xPos + (_xPos * 0.3f),0,_zPos);
        }
    }
    private void Start()
    {
        startPos = transform.position;
        currentPosX = transform.position.x;
        CreatorGrid();
    }
    private void Update()
    {
        if(!canSpread && !spreadStart)
        {
            currentPosX += Time.deltaTime * speed * dir;
            if (currentPosX >= maxMoveX)
            {
                dir *= -1;
                currentPosX = maxMoveX;
            }
            else if (currentPosX <= -maxMoveX)
            {
                dir *= -1;
                currentPosX = -maxMoveX;
            }
            transform.position = new Vector3(currentPosX, startPos.y, startPos.z);
        }
        if (canSpread) 
        {
            currentSpread += Time.deltaTime * spreadDir * spreadSpeed;
            if(currentSpread>=spreadAmount|| currentSpread <= 0) 
            {
                spreadDir *= -1;
            }
            for (int i = 0; i < enemyFormations.Count; i++)
            {
                if(Vector3.Distance(enemyFormations[i].objectEnemy.transform.position, enemyFormations[i].goal)>= 0.001f) 
                {
                    enemyFormations[i].objectEnemy.transform.position = Vector3.Lerp(transform.position + enemyFormations[i].start, transform.position + enemyFormations[i].goal, currentSpread);

                }
            }
        }
        /*if (canDive) 
        {
            Invoke("SetDiving", Random.Range(3, 10));
            canDive = false;
        }*/

    }


    public IEnumerator ActiveSpread() 
    {
        if (spreadStart) 
        {
            yield break;
        }
        spreadStart = true;
        while(transform.position.x != startPos.x) 
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            yield return null;
        }
        canSpread = true;
        //canDive = true;
        Invoke("SetDiving", Random.Range(3, 10));
    }

/*private void OnDrawGizmos()
    {
        gridList.Clear();

        int number = 0;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                float x = (gridOffSetX +gridOffSetX * 2 * (number/div)) * Mathf.Pow(-1, number %2 + 1);
                float z = gridOffSetZ * ((number%div)/2);

                Vector3 vector = new Vector3(this.transform.position.x + x, 0, this.transform.position.z + z);
                Handles.Label(vector, number.ToString());
                number++;
                gridList.Add(vector);
            }
        }
    }*/

    void CreatorGrid() 
    {
        {
            gridList.Clear();

            int number = 0;
            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    float x = (gridOffSetX + gridOffSetX * 2 * (number / div)) * Mathf.Pow(-1, number % 2 + 1);
                    float z = gridOffSetZ * ((number % div) / 2);

                    Vector3 vector = new Vector3(x, 0,z);

                    //Handles.Label(vector, number.ToString());
                    number++;
                    gridList.Add(vector);
                }
            }
        }
    }
    public Vector3 GetVector(int ID) 
    {
        return transform.position + gridList[ID];
    }

    void SetDiving() 
    {
        if(enemyFormations.Count > 0) 
        {
            int choosenPatch = Random.Range(0, divePatchList.Count);
            int choosenEnemy = Random.Range(0, enemyFormations.Count);

            GameObject newPatch = Instantiate(divePatchList[choosenPatch], enemyFormations[choosenEnemy].start + transform.position, Quaternion.identity) as GameObject;
            enemyFormations[choosenEnemy].objectEnemy.GetComponent<Enemy>().DiveSetup(newPatch.GetComponent<Patch>());
            enemyFormations.RemoveAt(choosenEnemy);
            Invoke("SetDiving", Random.Range(3,10));
        }
        else 
        {
            CancelInvoke("SetDiving");
            return;
        }
    }

}
