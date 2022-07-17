using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySprite : MonoBehaviour
{
    [SerializeField] ItemSO item;

    public void OnItemSelected() {
        // do stuff - check game state in GameManager,
        // probably
        Debug.Log("Yoooo");
        string gameState = GameManager.Instance.GetGameState();

        if (gameState == "bar") {
            HandleBarItemSelected();
        } else if (gameState == "home") {
            HandleHomeItemSelected();
        }
    }

    void HandleBarItemSelected() {
        // do stuff?
    }

    void HandleHomeItemSelected() {
        CanvasController canvasController = FindObjectOfType<CanvasController>();
        canvasController.SelectItem(item);
    }
}
