using UnityEngine;

public class Card : MonoBehaviour
{
    int cardId;
    public SpriteRenderer cardFront;
    public Animator anim;

    public void SetCard(int id, Sprite sprite)
    {
        cardId = id;
        cardFront.sprite = sprite;
    }
    public void FlipOpen(bool fliped)
    {
        anim.SetBool("Flip", fliped);
    }
    public int GetCardId()
    {
        return cardId;
    }
}
