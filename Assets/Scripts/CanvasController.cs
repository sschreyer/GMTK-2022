using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameObject momButton;
    [SerializeField] GameObject dadButton;
    [SerializeField] GameObject brotherButton;

    [SerializeField] GameObject sellButton;

    List<ItemSO> itemSprites = new List<ItemSO>();
    ItemSO currSelecItem = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValueText();

        // TODO: Update family text here in future...
        UpdateFamilyText();

        // In round one, all family buttons are disabled...
        // assume this by default
    }

    private void UpdateFamilyText() {
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
