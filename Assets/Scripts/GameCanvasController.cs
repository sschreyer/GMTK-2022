using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInGameItem(GameObject sprite) {
        GameObject real_sprite = Instantiate(sprite);
        real_sprite.transform.parent = this.gameObject.transform; 
    }
}
