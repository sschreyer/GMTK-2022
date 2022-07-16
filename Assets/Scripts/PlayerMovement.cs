using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;

    Rigidbody2D rigidBody;
    CircleCollider2D bodyCollider;

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
    }

    void Run() {
        Vector2 playerVelocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
        rigidBody.velocity = playerVelocity * runSpeed;
    }

    // void Die() 
    // {
    //     if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Sightline")))
    //     {
    //         Debug.Log("Aghhhh we would'ved DIED!!!");
    //     }
    // }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            Debug.Log("Aghhhh we would'ved DIED!!!");
        }
    }
}
