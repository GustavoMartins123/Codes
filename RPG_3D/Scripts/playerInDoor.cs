using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class playerInDoor : MonoBehaviour
{
    public Animator anim;
    public bool open;
    int goldAmount;
    [SerializeField] Collider chest;
    [SerializeField] Text amount;
    [SerializeField] Transform spawn;
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] GameObject canvas;
    private void Start()
    {
        if(gameObject.CompareTag("chest"))
        {
            goldAmount = Random.Range(30, 250);
            amount.text = goldAmount + "+";
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("chest") && Open_Inventory.key > 0)
        {
            Open_Inventory.key--;
            anim.SetBool("Open", true);
            canvas.SetActive(true);
            Open_Inventory.gold += goldAmount;
            particleEffect.Play();
            goldAmount = 0;
            AudioManager.instance.Sounds(17);
            Destroy(canvas, 3f);
            Destroy(chest);
        }
    }
}
