using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyVisible : MonoBehaviour
{
    Outline outline;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        //StartCoroutine(Player());
        player = PlayerDisplay.instance.players[Save.pchar].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 offset = player.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
            if (sqrLen < 25*2)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }
        }
    }
}
