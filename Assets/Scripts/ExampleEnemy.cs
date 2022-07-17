using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : MonoBehaviour
{
    bool isWaiting = false;
    
    void Update() {
        if (!isWaiting) {
            StartCoroutine(Wait());
            isWaiting = true;
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {
        // add to list of watchers
        if (other.gameObject.tag == "Player") {
            Debug.Log("Entering view");
            GameManager.Instance.AddWatcher(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        // remove from list of watchers
        if (other.gameObject.tag == "Player") {
            GameManager.Instance.RemoveWatcher(this.gameObject);
        }
    }

    IEnumerator Wait()
    {
        int s = Random.Range(2,7);
        yield return new WaitForSeconds(s);

        int n = Random.Range(0,3);
        Quaternion turn = Quaternion.Euler(0, 0, 45 + (90 * n));
        transform.rotation = turn;
        isWaiting = false;
    }
}
