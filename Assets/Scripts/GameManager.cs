using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<ItemSO> items;
    List<GameObject> itemRefs = null;
    int money; 
    int whichDay = 0; 
    string gameState; 
    bool isStealing = false;
    List<GameObject> watchers = new List<GameObject>();
    // state variables are stored as the conditon 
    // and "turns till death"
    // how bad each condition is is dealt with in the 
    // CanvasController 
    Tuple<string, int> momState;
    Tuple<string, int> dadState;
    Tuple<string, int> brotherState;

    

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
        momState = new Tuple<string, int>("healthy", -1);
        dadState = new Tuple<string, int>("healthy", -1);
        brotherState = new Tuple<string, int>("healthy", -1);
    }

    void Update() {
        if (itemRefs != null && itemRefs.Count == 0) {
            EndDay();
        }
    }

    public void StartGame()
    {
        gameState = "bar";
        SceneManager.LoadScene(1);
    }

    public void EndDay() 
    {
        itemRefs = null;
        gameState = "home";
        watchers = new List<GameObject>();
        SceneManager.LoadScene(2);
    }

    public void NextDay() 
    {
        PrintItems();
        whichDay++;
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

    public void SubtractMoney(int money) {
        this.money -= money;
    }

    public void RemoveItem(ItemSO item) {
        items.Remove(item);
    }

    public void Steal() {
        this.isStealing = true;

        // remove a reference to some item 
        itemRefs.RemoveAt(0);

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

    public int GetWhichDay() {
        return whichDay;
    }

    public void UpdateFamilyState(int member, Tuple<string, int> state) {
        
        switch(member) {
            case 0:
                Debug.Log("update mom with " + state.Item1);
                momState =  new Tuple<string,int>(state.Item1, state.Item2);
                break;
            case 1:
                Debug.Log("update dad with " + state.Item1);
                dadState =  new Tuple<string,int>(state.Item1, state.Item2);
                break;
            case 2: 
                Debug.Log("update brother with " + state.Item1);
                brotherState = new Tuple<string,int>(state.Item1, state.Item2);
                break; 
            default:
                break;
        }
    }

    public Tuple<string, int> GetFamilyState(int member) {
         switch(member) {
            case 0:
                return momState;
            case 1:
                return dadState;
            case 2: 
                return brotherState; 
            default:
                return null;
        }
    }

    public void AddItems(List<GameObject> items) {
        this.itemRefs = items;
    }
}
