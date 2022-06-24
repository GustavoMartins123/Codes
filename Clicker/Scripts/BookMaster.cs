using UnityEngine;
public class BookMaster : MonoBehaviour
{
    public static int level = 1;
    Animator anim;
    [SerializeField]ParticleSystem parti;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (parti.isPlaying)
        {
            Pause.ispaused = false;
        }
    }
    public void Click() 
    {
        GameManager.instance.AddMoney(level);
        anim.SetTrigger("click");
        parti.Play();
    }
}
