using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;



public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject inventoryUI;

    public Transform content;
    public GameObject slot;

    public void Update()
    {
        // Open Inventory
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
            InventoryItem newItem = gameObject.AddComponent<InventoryItem>();
            newItem.item = item;
            newItem.quantity = quantity;
            items.Add(newItem);
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
        UpdateInventoryUI();
    }

    public void DropItem(Item item, int quantity)
    {
        Debug.Log("Dropping item");
        InventoryItem existingItem = items.Find(i => i.item == item);

        if (existingItem != null)
        {
            existingItem.quantity -= quantity;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }
        Vector3 spawnPosition = transform.position + transform.forward * 2.0f + transform.up;
        Instantiate(existingItem.item.prefab, spawnPosition, Quaternion.identity);
        UpdateInventoryUI();
    }

    public void UseItem(Item item, int quantity)
    {
        Debug.Log("Using item");
        InventoryItem existingItem = items.Find(i => i.item == item);

        if (existingItem != null)
        {
            existingItem.quantity -= quantity;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }

        Vector3 spawnPosition = transform.position - transform.up * 100f;
        GameObject obj = Instantiate(existingItem.item.prefab, spawnPosition, Quaternion.identity);
        if (obj.TryGetComponent(out IUseable usableObject))
        {
            usableObject.Use();
        }

        UpdateInventoryUI();
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
            var invetoryItem = obj.GetComponent<InventoryItem>();
            invetoryItem.item = inv.item;
            invetoryItem.inventoryReference = this;
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
