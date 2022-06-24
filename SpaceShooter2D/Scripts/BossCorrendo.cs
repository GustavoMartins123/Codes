using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCorrendo : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Morrendo() 
    {
        var meuPai = transform.parent.gameObject;
        Destroy(meuPai);
    }
    private void OnDestroy()
    {
        Instantiate(boss, transform.position, transform.rotation);
    }
}
