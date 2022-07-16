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

    }
}
