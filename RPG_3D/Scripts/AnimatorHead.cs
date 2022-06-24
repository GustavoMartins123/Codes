using System.Collections;
using UnityEngine;

public class AnimatorHead : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject player;

    private void OnEnable()
    {
        if (player == null)
        {
            //StartCoroutine(SearchPlayer());
            player = PlayerDisplay.instance.players[Save.pchar];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    void OnAnimatorIK(int layerIndex)
    {
        if(player!= null)
        {
            anim.SetLookAtWeight(0.43f);
            anim.SetLookAtPosition(player.transform.position + Vector3.up);
        }
    }
}
