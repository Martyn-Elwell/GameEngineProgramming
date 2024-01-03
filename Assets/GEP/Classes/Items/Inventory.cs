using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        Item existingItem = items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.quantity += item.quantity;
        }
        else
        {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        Item existingItem = items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.quantity -= item.quantity;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }
    }
}
