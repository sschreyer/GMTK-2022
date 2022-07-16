using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<ItemSO> items;
    int money; 
    int whichDay; 

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
        money = 0;
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

    public int GetTotalValue() {
        int total = 0;
        foreach (ItemSO itemSO in items) {
            total += itemSO.GetValue();
        }
        return total;
    }

    public int GetMoney() {
        return money;
    }

    public void AddInGameItem(GameObject sprite) {
        // sprite is a UI image, essentially. 
        GameCanvasController gameCC = FindObjectOfType<GameCanvasController>();
        gameCC.AddInGameItem(sprite);
    }
}
