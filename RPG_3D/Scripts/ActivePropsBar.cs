using UnityEngine;

public class ActivePropsBar : MonoBehaviour
{
    [SerializeField] GameObject props;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            props.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            props.SetActive(false);
        }
    }
}
