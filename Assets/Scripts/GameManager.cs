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
    string gameState; 
    bool isStealing = false;
    List<GameObject> watchers = new List<GameObject>();

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
        gameState = "bar";
        SceneManager.LoadScene(1);
    }

    public void EndDay() 
    {
        gameState = "home";
        watchers = new List<GameObject>();
        SceneManager.LoadScene(2);
    }

    public void NextDay() 
    {
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

        // We have a max inv size
        if (items.Count >= 10) return;

        items.Add(item);
        AddInGameItem(item.GetSprite());
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

    public string GetGameState() {
        return gameState;
    }

    public void DisplayInGameItems() {
        GameCanvasController gameCC = FindObjectOfType<GameCanvasController>();
        foreach(ItemSO item in items) {
            gameCC.AddInGameItem(item.GetSprite());
        }
    }

    public void AddInGameItem(GameObject sprite) {
        // sprite is a UI image, essentially. 
        GameCanvasController gameCC = FindObjectOfType<GameCanvasController>();
        gameCC.AddInGameItem(sprite);
    }

    public void AddMoney(int money) {
        this.money += money;
    }

    public void RemoveItem(ItemSO item) {
        items.Remove(item);
    }

    public void Steal() {
        this.isStealing = true;
        // perform a check to see if we're in view of anyone
        if (watchers.Count > 0) {
            EndDay();
        }

        // set timeoout for stealing - 
        // note I don't really think this does much yet?
        // may need to modify AddWatcher???
        StartCoroutine(Hide());        
    }

    public bool GetIsStealing() {
        return isStealing;
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.1f);
        isStealing = false;
    }

    public void AddWatcher(GameObject watcher) {
        watchers.Add(watcher);
    }

    public void RemoveWatcher(GameObject watcher) {
        watchers.Remove(watcher);
    }
}
