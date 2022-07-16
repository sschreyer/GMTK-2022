using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<ItemSO> items;
    int a; 

    void Awake() {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Duplicate GameManager created every time the scene is loaded
            Destroy(gameObject);
        }
    }

    void Start() {
        items = new List<ItemSO>();
        a = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NextDay() {
        PrintItems();
        SceneManager.LoadScene(1);
    }


    public void AddItem(ItemSO item) 
    {   
        Debug.Log("Adding item");
        if (item == null ) {
            Debug.Log("Item is null!");
            return; 
        }
        items.Add(item);
        // ItemSO new_item = Instantiate(item);
        // DontDestroyOnLoad(new_item);
        // Debug.Log(new_item);
        // this.items.Add(new_item);
    }

    public void SetA(int a) {
        this.a = a;
    }

    public List<ItemSO> GetItems() 
    {
        return items;
    }

    public void PrintItems() {

        Debug.Log(a);
        foreach (ItemSO itemSO in items) {
            Debug.Log(itemSO.GetItem());
        }
    }

    // public InventorySO GetInventory() {
    //     return inventory;
    // }

    // public void SetInventory(InventorySO inventory) {
    //     this.inventory = inventory;
    // }
}
