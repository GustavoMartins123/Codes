using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int id;
    public void BuyAnItem() 
    {
         GameManager.instance.BuyItem(id);
         GameManager.instance.SaveOnBuy();

    }
}
