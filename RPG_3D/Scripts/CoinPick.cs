using UnityEngine;
using UnityEngine.UI;

public class CoinPick : MonoBehaviour
{
    int goldAmount;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject[] coinsG;
    [SerializeField] Text coins;
    Collider colliderB;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        goldAmount = Random.Range(15, 50);
        coins.text = goldAmount + "+";
        for (int i = 0; i < 20; i++)
        {
            coinsG[i] = gameObject.transform.GetChild(i).gameObject;
        }
        colliderB = GetComponent<Collider>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (canvas.activeSelf)
        {
            canvas.transform.LookAt(cam.transform.position);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Open_Inventory.gold += goldAmount;
            AudioManager.instance.Sounds(17);
            canvas.SetActive(true);
            colliderB.enabled = false;
            for (int i = 0; i < 20; i++)
            {
                coinsG[i].SetActive(false);
            }
            Destroy(gameObject, 3f);
        }
    }
}
