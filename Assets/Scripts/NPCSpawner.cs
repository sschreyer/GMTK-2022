using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject npc;

    void Start() {
        int numChildren = transform.childCount;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
    
        List<int> prevNumbers = new List<int>();
        // we make 
        for (int i = 0; i < 4; i++) {
            int n = Random.Range(0, numChildren);
            while (prevNumbers.Contains(n)) {
                n = Random.Range(0, numChildren);
            }
            prevNumbers.Add(n);
            // set it to the location of the nth child
            GameObject newNpc = Instantiate(npc, children[n]);
        }  
    }
}
