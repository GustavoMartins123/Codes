using System.Collections.Generic;
using UnityEngine;
public class CardManager : MonoBehaviour
{
    int d = 0;
    [HideInInspector]public int pairAmount;
    public List<Sprite> sprites = new List<Sprite>();

    public float offset;
    public GameObject cardPrefab;

    List<GameObject> carDeck = new List<GameObject>();
    [HideInInspector]public int width;
    [HideInInspector]public int height;
    // Start is called before the first frame update
    void Start()
    {
        d = PlayerPrefs.GetInt("difi", 1);
        switch (d)
        {
            case 1:
                ScoreManager.instance.timeForLevelComplete = 306;
                break;
            case 2:
                ScoreManager.instance.timeForLevelComplete = 246;
                break;
            case 3:
                ScoreManager.instance.timeForLevelComplete = 156;
                break;
            case 4:
                ScoreManager.instance.timeForLevelComplete = 126;
                break;

        }
        GameManager.instance.SetPairs(pairAmount);
        CreatePlayField();
    }

    void CreatePlayField()
    {
        List<Sprite> tempList = new List<Sprite>();
        tempList.AddRange(sprites);
        for (int i = 0; i < pairAmount; i++)
        {
            int randSprite = Random.Range(0, tempList.Count);
            for (int j = 0; j < 2; j++)
            {
                Vector3 pos = Vector3.zero;
                GameObject newCard = Instantiate(cardPrefab, pos, Quaternion.identity) as GameObject;
                
                newCard.GetComponent<Card>().SetCard(i, tempList[randSprite]);

                carDeck.Add(newCard);
            }
            tempList.RemoveAt(randSprite);
        }
        //Shuffle
        for (int i = 0; i < carDeck.Count; i++)
        {
            int index = Random.Range(0, carDeck.Count);
            var temp = carDeck[i];
            carDeck[i] = carDeck[index];
            carDeck[index] = temp;
        }
        //Pass out cards on field
        int num = 0;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * offset, 0, z * offset);
                carDeck[num].transform.position = pos;
                num++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * offset, 0, z * offset);
                Gizmos.DrawWireCube(pos, new Vector3(1, 0.1f, 1));
            }
        }
    }
}
