using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveBoss : MonoBehaviour
{
    public static ReviveBoss revive;
    public ZombieMovement[] zombieMovement;

    private void Awake()
    {
        revive = this;
    }
    public void Revive(int i)
    {
        if(zombieMovement[i].iamDragon && Open_Inventory.instance.objectives[3]|| zombieMovement[i].iamSpider && Open_Inventory.instance.objectives[4])
        {
            zombieMovement[i].morto = false;
            zombieMovement[i].agent.isStopped = false;
            zombieMovement[i].health = 1f;
            zombieMovement[i].healthBar.fillAmount = zombieMovement[0].health;
            zombieMovement[i].colPr.enabled = true;
            zombieMovement[i].player = PlayerDisplay.instance.players[Save.pchar];
            zombieMovement[i].anim.SetTrigger("revive");
            zombieMovement[i].StartCoroutine(zombieMovement[i].chamaPatrulha());
        }
    }
}
