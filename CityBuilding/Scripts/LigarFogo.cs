using UnityEngine;
using UnityEngine.VFX;
public class LigarFogo : MonoBehaviour
{
    [SerializeField]VisualEffect visualEffect;
    bool tenhoRecurso = false;
    private void Awake()
    {
        visualEffect = GetComponentInChildren<VisualEffect>();
    }
    private void Update()
    {
        if (GameManager.instance.totalWood <= 0)
        {
            visualEffect.enabled = false;
            tenhoRecurso = false;
        }
        else if (GameManager.instance.totalWood > 0) 
        {
            visualEffect.enabled = true;
            tenhoRecurso = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && tenhoRecurso == true) 
        {
            visualEffect.enabled = true;
        }
    }
}
