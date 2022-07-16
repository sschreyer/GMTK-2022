using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class InventorySO : ScriptableObject
{
    // [TextArea(2,6)]
    // [SerializeField] string name = "Enter new item here";
    // [SerializeField] int value = 0;
    List<ItemSO> items = new List<ItemSO>();

    public void AddItem(ItemSO item) 
    {   
        items.Add(item);
    }

    public List<ItemSO> GetItems() 
    {
        return items;
    }

    public void PrintItems() {
        foreach (ItemSO itemSO in items) {
            Debug.Log(itemSO.GetItem());
        }
    }
}
