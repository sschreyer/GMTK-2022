using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<ItemSO> items;
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

    public int GetWhichDay() {
        return whichDay;
    }

    public void UpdateFamilyState(int member, Tuple<string, int> state) {
        switch(member) {
            case 0:
                momState = state;
                break;
            case 1:
                dadState = state;
                break;
            case 2: 
                brotherState = state;
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
                break;
        }
    }
}
