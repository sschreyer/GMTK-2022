using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject item;

    void Start() {
        int numChildren = transform.childCount;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
    
        List<int> prevNumbers = new List<int>();

        if (numChildren <= 0 ) {
            Debug.Log("aghh");
        }
        // we make 
        for (int i = 0; i < 4; i++) {
            int n = Random.Range(0, numChildren);
            while (prevNumbers.Contains(n)) {
                n = Random.Range(0, numChildren);
            }
            prevNumbers.Add(n);
            // set it to the location of the nth child
            GameObject newNpc = Instantiate(item, children[n]);
        }   
    } 
}
