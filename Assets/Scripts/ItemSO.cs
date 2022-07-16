using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string item = "Enter new item here";
    [SerializeField] int value = 0;

    public string GetItem() 
    {
        return item;
    }

    public int GetValue() 
    {
        return value;
    }
}
