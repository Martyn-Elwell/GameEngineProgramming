using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int quantity;
    public Inventory inventoryReference;
    public bool slot = false;
    public int slotID;
    public Type slotType;

    InventoryItem(Item item, int quantity, Inventory inventoryReference)
    {
        this.item = item;
        this.quantity = quantity;
        this.inventoryReference = inventoryReference;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Left click
            if (slot)
            {
                //Select Slot
                inventoryReference.SelectSlot(gameObject.GetComponent<InventoryItem>(), slotID);
            }
            if (!slot && item != null)
            {
                if (item.type == Type.Useable)
                {
                    inventoryReference.UseItem(item, 1);
                }
                else
                {
                    //Select item
                    inventoryReference.SelectItem(gameObject.GetComponent<InventoryItem>());
                }
            }
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

