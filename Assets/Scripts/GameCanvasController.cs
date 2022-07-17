using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    List<GameObject> sprites = new List<GameObject>();

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
        // we should offset it based off it's position in the list?
        real_sprite.transform.parent = this.gameObject.transform; 
        // Note that 40 is an arbitrary constant I picked...
        Vector2 newPos = new Vector2(-920 +  sprites.Count * 100, -500);
        real_sprite.transform.localPosition = newPos;
        sprites.Add(sprite);
    }
}
