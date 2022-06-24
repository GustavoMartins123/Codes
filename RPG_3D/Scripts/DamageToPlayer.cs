using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Save.invincibility == false)
        {
            other.GetComponent<Player>().Damage(0.3f);
            //Player.anim.SetTrigger("hit");
        }
    }
}
