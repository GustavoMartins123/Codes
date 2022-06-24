using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float SpawRate;
    [SerializeField] int contagem = 0;
    private float nextSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            if (contagem < 5)
            {
                nextSpawn = Time.time + SpawRate;
                contagem++;
                Instantiate(enemy, transform.position, enemy.transform.rotation);
            }
        }
    }
    public void DiminuiInimigo()
    {
        contagem--;
    }
}
