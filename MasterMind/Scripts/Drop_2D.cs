using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop_2D : MonoBehaviour, IDropHandler
{
    public Dragging_Box dragging_Box;
    public int slotId;
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = transform.position;
        GameObject newPearl = Instantiate(eventData.pointerDrag.gameObject, transform, true);
        newPearl.transform.position = transform.position;
        dragging_Box.SetId(slotId, eventData.pointerDrag.GetComponent<Drag_2D>().pearl_Id);
    }


}
