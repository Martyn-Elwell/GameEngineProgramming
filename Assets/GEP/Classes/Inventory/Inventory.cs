using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Text = TMPro.TextMeshProUGUI;



public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<InventoryItem> slotItems = new List<InventoryItem>();
    public List<InventoryItem> currentCraftingItems = new List<InventoryItem>();
    public List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();
    public GameObject inventoryUI;
    public InventoryItem selectedItem;
    public InventoryItem selectedSlot;

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
        // Adds Item from Inventory
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
        // Removes Item from Inventory
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
        bool inSlot = false;
        InventoryItem existingItem = items.Find(i => i.item == item);

        // Checks if Item in Item Slot
        if (existingItem = slotItems.Find(i => i.item == item))
        {
            inSlot = true;
        }
        //Removes Item from Item slot
        if (inSlot)
        {
            slotItems[existingItem.slotID].item = null;
            selectedItem = null;
            selectedSlot = null;
        }

        // Removes Item from Inventory
        if (existingItem != null && !inSlot)
        {
            existingItem.quantity -= quantity;

            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
            
        }

        // Spawn Gameworld Object
        Vector3 spawnPosition = transform.position + transform.forward * 2.0f + transform.up;
        Instantiate(existingItem.item.prefab, spawnPosition, Quaternion.identity);
        
        UpdateInventoryUI();
    }

    public void UseItem(Item item, int quantity)
    {
        // Use Item if useable
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


    public void MoveItemToSlot()
    {
        // Move the item to the selected empty slot
        if (selectedSlot != null)
        {
            if (selectedSlot.item != null)
            {
                AddItem(selectedSlot.item, 1);
            }
            if (selectedSlot.slotType == selectedItem.item.type)
            {
                selectedSlot.item = selectedItem.item;
                selectedSlot.quantity = 1;
                RemoveItem(selectedItem.item, 1);
            }
        }
        selectedSlot = null;
        selectedItem = null;
        UpdateInventoryUI();
    }

    public void MoveItemFromSlot()
    {
        // Move the item from the selected empty slot back to the inventory
        if (selectedSlot != null && selectedSlot.item != null)
        {
            AddItem(selectedSlot.item, selectedSlot.quantity);
            selectedSlot.item = null;
            selectedSlot.quantity = 0;
        }
        selectedSlot = null;
        selectedItem = null;
        UpdateInventoryUI();
    }

    public void SelectItem(InventoryItem inventoryItem)
    {
        // Selects Item from inventory
        selectedItem = items.Find(i => i.item == inventoryItem.item);
        Debug.Log("Selected item");
        if (selectedSlot != null)
        {
            MoveItemFromSlot();
        }
        else
        {
            Debug.Log("but no slot found");
        }
    }

    public void SelectSlot(InventoryItem emptySlot, int slotID)
    {
        // Selects Item Slot
        selectedSlot = slotItems[slotID];
        Debug.Log("Selected slot");
        if (selectedItem != null)
        {
            Debug.Log("Moving item");
            MoveItemToSlot();
        }
        else
        {
            Debug.Log("removing item");
            MoveItemFromSlot();
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
            // Setup slot
            GameObject obj = Instantiate(slot, content);
            var invetoryItem = obj.GetComponent<InventoryItem>();
            invetoryItem.item = inv.item;
            invetoryItem.inventoryReference = this;

            // Find elements
            var itemName = obj.transform.Find("Text").GetComponent<Text>();
            var itemImage = obj.transform.Find("Image").GetComponent<Image>();
            var itemCount = obj.transform.Find("Count").GetComponent<Text>();

            // Assign elements
            itemName.text = inv.item.itemName;
            itemImage.sprite = inv.item.icon;
            if (inv.quantity > 1) { itemCount.text = inv.quantity.ToString(); }
            else { itemCount.text = ""; }

        }


        foreach (var inv in slotItems)
        {
            if (inv.item != null)
            {
                GameObject obj = inv.gameObject;
                var itemName = obj.transform.Find("Text").GetComponent<Text>();
                var itemImage = obj.transform.Find("Image").GetComponent<Image>();
                var ItemOutline = obj.transform.Find("Outline").GetComponent<Image>();

                itemImage.color = new Color32(255, 255, 225, 255);
                ItemOutline.color = new Color32(255, 255, 225, 0);
                itemName.text = inv.item.itemName;
                itemImage.sprite = inv.item.icon;
            }
            else if (inv.item == null)
            {
                GameObject obj = inv.gameObject;
                var itemName = obj.transform.Find("Text").GetComponent<Text>();
                var itemImage = obj.transform.Find("Image").GetComponent<Image>();
                var ItemOutline = obj.transform.Find("Outline").GetComponent<Image>();

                itemImage.color = new Color32(255, 255, 225, 0);
                ItemOutline.color = new Color32(255, 255, 225, 255);
                itemName.text = "";
            }
            
        }
    }

    public void Craft(Item ingredient1, Item ingredient2)
    {
        CraftingRecipe recipe = FindCraftingRecipe(ingredient1, ingredient2);

        if (recipe != null)
        {
            AddItem(recipe.result, 1);
            RemoveItem(ingredient1, 1);
            RemoveItem(ingredient2, 1);
        }
        else
        {
            Debug.LogWarning("Crafting recipe not found.");
        }
    }

    private CraftingRecipe FindCraftingRecipe(Item ingredient1, Item ingredient2)
    {
        foreach (CraftingRecipe recipe in craftingRecipes)
        {
            if ((recipe.ingredient1 == ingredient1 && recipe.ingredient2 == ingredient2) ||
                (recipe.ingredient1 == ingredient2 && recipe.ingredient2 == ingredient1))
            {
                return recipe;
            }
        }

        return null;
    }
}
