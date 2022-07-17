using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    ItemSO currSelecItem = null;
    const int MOM = 0;
    const int DAD = 1;
    const int BROTHER = 2;  

    // Start is called before the first frame update
    void Start()
    {
        UpdateValueText();

        UpdateInventory();


        // TODO: Update family text here in future...
        UpdateFamilyText();

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
        }
    }

    // TODO: implement
    private void UpdateFamilyText() {
        int whichDay = GameManager.Instance.GetWhichDay();

        // odds of a bad event should increase with each day 
        
        // get current state of family.
        for (int i = 0; i < 3; i ++) {
            Tuple<string, int> state = GameManager.Instance.GetFamilyState(i);
            string condition = state.Item1;
            Tuple<string, int> newState = new Tuple<string,int>(condition, state.Item2);
            string buttonText = "";
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

            UpdateFamilyMemberText(i, newState.Item1, buttonText);
        }

    }

    private void UpdateFamilyMemberText(int member, string condition, string buttonText) {
        TMP_Text buttonTextComponent;
        switch(member) {
            case MOM:
                buttonTextComponent = momButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                momText.text = "Your mom is " + condition;
                break;
            case DAD:
                buttonTextComponent = dadButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                dadText.text = "Your mom is " + condition;
                break; 
            case BROTHER:
                buttonTextComponent = brotherButton.GetComponentInChildren<TMP_Text>(true);
                buttonTextComponent.text = buttonText;
                brotherText.text = "Your mom is " + condition;
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
                return -1;
            default:
                Debug.Log("UNKNOWN CONDITION!!");
                return -1;
        }
    }

    private Tuple<string,int> RandomConditionGenerate(int whichDay) {
        Tuple<string, int> healthy = new Tuple<string,int>("healthy", -1);
        int n = 0;
        switch(whichDay) {
            case 1:
                return healthy;
                break;
            case 2:
                // 25% chance for any family member to go hungry
                n = UnityEngine.Random.Range(0,99);
                if (n <= 25 - 1) {
                    return new Tuple<string,int>("hungry", 3);
                }
                return healthy; 
            case 3:
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
        SceneManager.LoadScene(2);

        UpdateValueText();
    }

    void UpdateValueText() {
        int totalValue = GameManager.Instance.GetTotalValue();
        totalText.text = "Total Value: " + totalValue.ToString();

        int money = GameManager.Instance.GetMoney();
        moneyText.text = "Money: " + money.ToString();
    }
}
