using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;

    // parts of the player
    Rigidbody2D rigidBody;
    CircleCollider2D bodyCollider;

    // data store 
    GameObject closestStealable = null; 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CircleCollider2D>();
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
            if (closestStealable == null) return; 

            // TODO: use scriptable objects or some kind of 
            // ENUM to store these? Not sure which, 
            // since we kinda need to attach a sprite 
            // to the obj as well as data.
            ExampleObjectScript exObjScr = closestStealable.GetComponent<ExampleObjectScript>();
            GameManager.Instance.AddItem(exObjScr.GetItem());
            GameManager.Instance.SetA(5);
            Debug.Log("Stolen " + closestStealable.name + "!!");
            Destroy(closestStealable);
            closestStealable = null;
        }
    }

    public void SetClosestStealable(GameObject obj) {
        Debug.Log("obj " + obj);
        this.closestStealable = obj;
    }

    
}
