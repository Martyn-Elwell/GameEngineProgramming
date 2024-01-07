using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;


[Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject inventoryUI;

    public Transform content;
    public GameObject slot;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Cursor.visible = inventoryUI.activeSelf;
            if (inventoryUI.activeSelf )
            {
                Cursor.lockState = CursorLockMode.None;
                UpdateInventoryUI();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void AddItem(Item item, int quantity)
    {
        InventoryItem existingItem = items.Find(i => i.item == item);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem { item = item, quantity = quantity });
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(Item item, int quantity)
    {
        InventoryItem existingItem = items.Find(i => i.item == item);

        if (existingItem != null)
        {
            existingItem.quantity -= quantity;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }
    }

    public void UpdateInventoryUI()
    {
        //Clean before opening
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
        foreach (var inv in items)
        {
            GameObject obj = Instantiate(slot, content);
            var itemName = obj.transform.Find("Text").GetComponent<Text>();
            var itemImage = obj.transform.Find("Image").GetComponent<Image>();
            var itemCount = obj.transform.Find("Count").GetComponent<Text>();

            itemName.text = inv.item.itemName;
            itemImage.sprite = inv.item.icon;
            if (inv.quantity > 1) { itemCount.text = inv.quantity.ToString(); }
            else { itemCount.text = ""; }

        }
    }
}
