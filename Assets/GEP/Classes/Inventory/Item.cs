using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colour
{
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Purple,
    Pink,
    White,
    Grey,
    Black
}

public enum Type
{
    Gem,
    Useable,
    Sword,
    Shield,
    Chestplate,
    Helmet
}


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Colour colour;
    public Type type;
    public Sprite icon;
    public GameObject prefab;
    public int quantity;
}
