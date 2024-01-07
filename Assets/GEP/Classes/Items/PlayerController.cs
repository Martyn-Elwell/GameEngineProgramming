using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inventory playerInventory;

    // Example function for picking up an item


    void OnCollisionEnter(Collision collision)
    {
        IPickupable pickupable = collision.gameObject.GetComponent<IPickupable>();
        if (pickupable != null)
        {
            pickupable.Pickup();

            Item item = collision.gameObject.GetComponent<ItemPickup>().item;
            if (item != null)
            {
                PickUpItem(item);
            }
        }
    }

    public void PickUpItem(Item item)
    {
        playerInventory.AddItem(item,item.quantity);
    }

    // Example function for using an item
    public void UseItem(Item item)
    {
        playerInventory.RemoveItem(item, item.quantity);
    }
}
