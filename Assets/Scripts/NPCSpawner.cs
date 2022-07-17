using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] GameObject npc1;
    [SerializeField] GameObject npc2;
    [SerializeField] GameObject npc3;
    [SerializeField] GameObject npc4;

    void Start() {
        int numChildren = transform.childCount;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
    
        List<int> prevNumbers = new List<int>();
        // set the upper bound of 1 based upon what day it is...
        int iUpperBound = 4; 
        switch(GameManager.Instance.GetWhichDay()) {
            case 0:
                iUpperBound = 4;
                break; 
            case 1:
                iUpperBound = 6;
                break; 
            case 2:
                iUpperBound = 8;
                break;
            case 3:
                iUpperBound = 10;
                break; 
            case 4:
                iUpperBound = 13; 
                break;
            case 5:
                GameManager.Instance.Win();
                break;
            default:
                break; 
        }

        // for my sanity
        if (iUpperBound > numChildren) {
            iUpperBound = numChildren;
        }

        for (int i = 0; i < iUpperBound; i++) {
            int n = Random.Range(0, numChildren);
            while (prevNumbers.Contains(n)) {
                n = Random.Range(0, numChildren);
            }
            prevNumbers.Add(n);

            GameObject enemy = null;
            int n2 = Random.Range(0, 4);
            switch(n2) {
                case 0:
                    enemy = npc;
                    break;
                case 1:
                    enemy = npc1;
                    break;
                case 2:
                    enemy = npc2;
                    break;
                case 3:
                    enemy = npc3;
                    break;
                case 4:
                    enemy = npc4;
                    break;
                default:
                    break;
            }

            // set it to the location of the nth child
            GameObject newNpc = Instantiate(enemy, children[n]);
        }  
    }
}
