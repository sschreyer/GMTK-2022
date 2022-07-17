using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI momText;
    [SerializeField] TextMeshProUGUI dadText;
    [SerializeField] TextMeshProUGUI brotherText;
    [SerializeField] GameObject momButton;
    [SerializeField] GameObject dadButton;
    [SerializeField] GameObject brotherButton;

    [SerializeField] GameObject sellButton;

    List<ItemSO> itemSprites = new List<ItemSO>();
    List<GameObject> itemObjects = new List<GameObject>();
    ItemSO currSelecItem = null;
    int momCost = 0;
    int dadCost = 0;
    int brotherCost = 0;
    const int MOM = 0;
    const int DAD = 1;
    const int BROTHER = 2;  

    // Start is called before the first frame update
    void Start()
    {
        UpdateValueText();

        UpdateInventory();


        // TODO: Update family text here in future...
        UpdateFamilyText(true);

        // In round one, all family buttons are disabled...
        // assume this by default
    }

    private void UpdateInventory() {
        List<ItemSO> items = GameManager.Instance.GetItems();

        // why is this in this function?
        foreach (ItemSO item in items) {
            // get sprite, render on canvas
            GameObject real_sprite = Instantiate(item.GetSprite());
            // we should offset it based off it's position in the list?
            real_sprite.transform.parent = this.gameObject.transform; 
            // Note that 40 is an arbitrary constant I picked...
            Vector2 newPos = new Vector2(-920 +  itemSprites.Count * 100, -500);
            real_sprite.transform.localPosition = newPos;
            itemSprites.Add(item);
            itemObjects.Add(real_sprite);
        }
    }

    // TODO: implement
    private void UpdateFamilyText(bool updateFamilyCondition) {
        int whichDay = GameManager.Instance.GetWhichDay();
        string buttonText = "";

        // odds of a bad event should increase with each day 
        
        // get current state of family.
        for (int i = 0; i < 3; i++) {
            Tuple<string, int> state = GameManager.Instance.GetFamilyState(i);
            Tuple<string, int> newState;
            if (updateFamilyCondition) {
                newState = UpdateFamilyCondition(state, i, whichDay, ref buttonText);
            } else {
                newState = state;
                Debug.Log("yes, it's not resetting");
                Debug.Log(newState.Item1);
                Debug.Log("hii");
                buttonText = "$" + GetConditionCost(newState.Item1).ToString();
                Debug.Log("our button text is " + buttonText);
            }

            // edge case because of poor decisions
            if (newState.Item1 == "death") {
                buttonText = "";
            }

            UpdateFamilyMemberText(i, newState.Item1, buttonText);
        }

    }

    private Tuple<string,int> UpdateFamilyCondition(Tuple<string,int> state, int i, int whichDay, ref string buttonText) {
        string condition = state.Item1;
        Tuple<string, int> newState = new Tuple<string,int>(condition, state.Item2);
        Debug.Log("Updating family condition!!!");
        if (condition != "healthy") {
            int turnsToLive = state.Item2 - 1;
            
            if (turnsToLive < 0) {
                condition = "dead";
            }
            buttonText = "$" + GetConditionCost(condition).ToString();
        } else {
            newState = RandomConditionGenerate(whichDay);
            buttonText = "$" + GetConditionCost(newState.Item1).ToString();
        }
        GameManager.Instance.UpdateFamilyState(i, newState);

        // set cost (jank but idc)
        switch(i) {
            case MOM:
                momCost = GetConditionCost(newState.Item1);
                break;
            case DAD:
                dadCost = GetConditionCost(newState.Item1);
                break;
            case BROTHER:
                brotherCost = GetConditionCost(newState.Item1);
                break;
            default:
                break;
        }
        return newState;
    }

    private void UpdateFamilyMemberText(int member, string condition, string buttonText) {
        TMP_Text buttonTextComponent;
        Button button;
        switch(member) {
            case MOM:
                buttonTextComponent = momButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                momText.text = "Your mom is " + condition;
                if (condition != "healthy" && condition != "dead") {
                    button = momButton.GetComponent<Button>();
                    button.interactable = true;
                }
                break;
            case DAD:
                buttonTextComponent = dadButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                dadText.text = "Your dad is " + condition;
                if (condition != "healthy" && condition != "dead") {
                    button = dadButton.GetComponent<Button>();
                    button.interactable = true;
                }
                break; 
            case BROTHER:
                buttonTextComponent = brotherButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                brotherText.text = "Your brother is " + condition;
                if (condition != "healthy" && condition != "dead") {
                    button = brotherButton.GetComponent<Button>();
                    button.interactable = true;
                }
                break;
            default:
                break;
        }
    }

    // functions as a bad way to store all conditions 
    // for our reference, as well as their cost to heal!
    private int GetConditionCost(string condition) {
        switch (condition) {
            case "hungry":
                return 50; 
            case "sick":
                return 75;
            case "very sick":
                return 100;
            case "dead":
                Debug.Log("Death can't be healed");
                return 0;
            case "healthy":
                Debug.Log("Healthy can't be healed");
                return 0;
            default:
                Debug.Log("UNKNOWN CONDITION " + condition);
                return -1;
        }
    }

    private Tuple<string,int> RandomConditionGenerate(int whichDay) {
        Tuple<string, int> healthy = new Tuple<string,int>("healthy", -1);
        int n = 0;
        Debug.Log("Hi, generate condition");
        switch(whichDay) {
            case 0:
                return healthy;
                break;
            case 1:
                // 25% chance for any family member to go hungry
                n = UnityEngine.Random.Range(0,99);
                if (n <= 25 - 1) {
                    return new Tuple<string,int>("hungry", 3);
                }
                return healthy; 
            case 2:
                n = UnityEngine.Random.Range(0,99);
                if (n <= 10 - 1) {
                    return new Tuple<string,int>("sick", 2);
                }

                if (n >= 10 && n <= 35 - 1) {
                    return new Tuple<string,int>("hungry", 3);
                }

                return healthy;
            default:
                return healthy;
        }
    }

    public void HandleFamilyPayment(int member) {
        int cost = 0; 
        Tuple<string,int> healthyState = new Tuple<string,int>("healthy", -1);
        switch(member) {
            case MOM:
                if (momCost > GameManager.Instance.GetMoney()) {
                    break;
                }
                cost = momCost;
                momCost = 0;
                GameManager.Instance.UpdateFamilyState(member, healthyState);
                GameManager.Instance.SubtractMoney(cost);
                break;
            case DAD:
                if (dadCost > GameManager.Instance.GetMoney()) {
                    break;
                }
                cost = dadCost;
                dadCost = 0;
                GameManager.Instance.UpdateFamilyState(member, healthyState);
                GameManager.Instance.SubtractMoney(cost);
                break; 
            case BROTHER:
                if (brotherCost > GameManager.Instance.GetMoney()) {
                    break;
                }
                cost = brotherCost;
                brotherCost = 0;
                GameManager.Instance.UpdateFamilyState(member, healthyState);
                GameManager.Instance.SubtractMoney(cost);
                break;
            default:
                break;
        }
        // reload 
        ResetText();
    }
 
    public void SelectItem(ItemSO item) {
        int value = item.GetValue();
        TMP_Text sellText = sellButton.GetComponentInChildren<TMP_Text>(true);
        sellText.text = "Sell item (" + value.ToString() + ")";
        currSelecItem = item;
    }

    public void SellCurrItem() {
        if (currSelecItem == null) return; 

        TMP_Text sellText = sellButton.GetComponentInChildren<TMP_Text>(true);
        sellText.text = "Sell item";
        GameManager.Instance.AddMoney(currSelecItem.GetValue());
        // currSelecItem = null;

        // TODO: remove item from inventory
        GameManager.Instance.RemoveItem(currSelecItem);
        // Reset Scene???
        ResetText();

        UpdateValueText();
    }

    void UpdateValueText() {
        int totalValue = GameManager.Instance.GetTotalValue();
        totalText.text = "Total Value: " + totalValue.ToString();

        int money = GameManager.Instance.GetMoney();
        moneyText.text = "Money: " + money.ToString();
    }

    private void ResetText() 
    {
        // have to reset inv
        foreach (GameObject obj in itemObjects) {
            Destroy(obj);
        }
        itemSprites = new List<ItemSO>();

        UpdateValueText();

        UpdateInventory();


        // TODO: Update family text here in future...
        UpdateFamilyText(false);
    }
}
