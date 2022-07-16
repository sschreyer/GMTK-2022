using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleEnemy : MonoBehaviour
{
    void Start() {
        StartCoroutine(Wait());        
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Handle "death"
        if (other.gameObject.tag == "Player") {
            Debug.Log("Aghhhh we would'ved DIED!!!");
            // gameManager.UpdateInventory(inventory);
            // idk if this should be here but it be what it be 
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator Wait()
    {
        int s = Random.Range(2,7);
        yield return new WaitForSeconds(s);
        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }
}
