using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        // Handle "death"
        if (other.gameObject.tag == "Player") {
            Debug.Log("Aghhhh we would'ved DIED!!!");
            // gameManager.UpdateInventory(inventory);
            // idk if this should be here but it be what it be 
            SceneManager.LoadScene(2);
        }
    }
}
