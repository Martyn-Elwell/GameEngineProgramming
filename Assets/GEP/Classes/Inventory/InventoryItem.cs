using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int quantity;
    public Inventory inventoryReference;
 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Left click
            inventoryReference.UseItem(item, 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            //Middle click
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Right click"
            inventoryReference.DropItem(item, 1);
        }
    }
}

