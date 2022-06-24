using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter_Test : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    int num;
    [SerializeField] Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        characters[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (num == 5)
            {
                num = 0;
                characters[num].SetActive(true);
                characters[5].SetActive(false);
                return;
            }
            characters[num].SetActive(false);
            num++;
            characters[num].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (num ==0)
            {
                num = 5;
                characters[num].SetActive(true);
                characters[0].SetActive(false);
                return;
            }
            characters[num].SetActive(false);
            num--;
            characters[num].SetActive(true);
        }



    }

    public void Next()
    {
        if (num == 5)
        {
            num = 0;
            characters[num].SetActive(true);
            characters[5].SetActive(false);
            return;
        }
        characters[num].SetActive(false);
        num++;
        characters[num].SetActive(true);
    }

    public void Back()
    {
        if (num == 0)
        {
            num = 5;
            characters[num].SetActive(true);
            characters[0].SetActive(false);
            return;
        }
        characters[num].SetActive(false);
        num--;
        characters[num].SetActive(true);
    }

    public void Accept()
    {
        Save.pchar = num;
        Save.pname = playerName.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
