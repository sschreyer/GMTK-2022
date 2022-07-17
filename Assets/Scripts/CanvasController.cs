using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameObject momButton;
    [SerializeField] GameObject dadButton;
    [SerializeField] GameObject brotherButton;

    List<GameObject> itemSprites = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int totalValue = GameManager.Instance.GetTotalValue();
        totalText.text = "Total Value: " + totalValue.ToString();

        int money = GameManager.Instance.GetMoney();
        moneyText.text = "Money: " + money.ToString();

        // TODO: Update family text here in future...
        UpdateFamilyText();

        // In round one, all family buttons are disabled...
        // assume this by default
    }

    private void UpdateFamilyText() {
        List<ItemSO> items = GameManager.Instance.GetItems();
        foreach (ItemSO item in items) {
            // get sprite, render on canvas
            GameObject real_sprite = Instantiate(item.GetSprite());
            // we should offset it based off it's position in the list?
            real_sprite.transform.parent = this.gameObject.transform; 
            // Note that 40 is an arbitrary constant I picked...
            Vector2 newPos = new Vector2(-920 +  itemSprites.Count * 100, -500);
            real_sprite.transform.localPosition = newPos;
            itemSprites.Add(real_sprite);
        }
    }
}
