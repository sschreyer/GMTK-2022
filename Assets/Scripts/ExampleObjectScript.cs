using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleObjectScript : MonoBehaviour
{
    [SerializeField] ItemSO item; 

    public ItemSO GetItem() {
        return item;
    }
}
