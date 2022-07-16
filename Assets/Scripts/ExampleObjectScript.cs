using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleObjectScript : MonoBehaviour
{
    [SerializeField] ItemSO item; 

    public ItemSO GetItem() {
        return item;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("I could steal it!");
            Debug.Log(this.gameObject);
            other.GetComponent<PlayerMovement>().SetClosestStealable(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("I can't steal it!");
            other.GetComponent<PlayerMovement>().SetClosestStealable(null);
        }
    }
}
