using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Recipe")]
public class CraftingRecipe: ScriptableObject
{
    public Item ingredient1;
    public Item ingredient2;
    public Item result;
}
