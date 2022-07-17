using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;

    // parts of the player
    Rigidbody2D rigidBody;
    CircleCollider2D bodyCollider;

    // data store 
    List<GameObject> closestStealable = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CircleCollider2D>();
        // we need to display in-game items somewhere,
        // may as well be here...
        GameManager.Instance.DisplayInGameItems();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        // Die();
        Steal();
    }

    void Run() {
        Vector2 playerVelocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
        rigidBody.velocity = playerVelocity * runSpeed;
    }

    void Steal() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (closestStealable.Count == 0) return; 

            // TODO: use scriptable objects or some kind of 
            // ENUM to store these? Not sure which, 
            // since we kinda need to attach a sprite 
            // to the obj as well as data.
            GameObject closestStealableObj = closestStealable[closestStealable.Count - 1];
            GameManager.Instance.Steal();
            ExampleObjectScript exObjScr = closestStealableObj.GetComponent<ExampleObjectScript>();
            GameManager.Instance.AddItem(exObjScr.GetItem());
            closestStealable.Remove(closestStealableObj);
            Destroy(closestStealableObj);
        }
    }

    public void AddClosestStealable(GameObject obj) {
        closestStealable.Add(obj);
    }

    public void RemoveClosestStealable(GameObject obj) {
        closestStealable.Remove(obj);
    }
}
