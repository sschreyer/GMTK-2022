using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] GameObject item4;

    [SerializeField] List<GameObject> items = new List<GameObject>();

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

            int n2 = Random.Range(0, 3);
            GameObject spawnItem = null;
            switch(n2) {
                case 0:
                    spawnItem = item;
                    break;
                case 1:
                    spawnItem = item2;
                    break;
                case 2:
                    spawnItem = item3;
                    break;
                case 3:
                    spawnItem = item4;
                    break;
                default:
                    break;
            }


            // set it to the location of the nth child
            GameObject newItem = Instantiate(spawnItem, children[n]);
            items.Add(newItem);
        }   

        GameManager.Instance.AddItems(items);
    } 
}
