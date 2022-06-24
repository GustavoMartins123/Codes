using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Text buttonText;
    [SerializeField] Color32 messageOff;
    [SerializeField] Color32 messageOn;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
    }

}
