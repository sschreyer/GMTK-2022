using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : MonoBehaviour
{
    bool isWaiting = false;
    bool facingLeft = false;
    bool facingUp = false;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }
    
    void Update() {
        if (!isWaiting) {
            StartCoroutine(Wait());
            isWaiting = true;
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {
        // add to list of watchers
        if (other.gameObject.tag == "Player") {
            Debug.Log("Entering view");
            GameManager.Instance.AddWatcher(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        // remove from list of watchers
        if (other.gameObject.tag == "Player") {
            GameManager.Instance.RemoveWatcher(this.gameObject);
        }
    }

    IEnumerator Wait()
    {
        int s = Random.Range(2,7);
        yield return new WaitForSeconds(s);

        int n = Random.Range(0,3);

        // // instead of this, create sprite?
        // PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 localScale = transform.localScale;
        Quaternion turn = Quaternion.Euler(0, 0, 45 + (90 * n));

        switch(n) {
            // "standard" idling
            case 0:
                if (facingLeft) {
                    localScale.x *= -1;
                }
                if (facingUp) {
                    localScale.y *= -1;
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                facingUp = false;
                facingLeft = false;
                animator.SetBool("isLooking", false);
                break;
            // flipped idling 
            case 1:
                if (!facingLeft) {
                    localScale.x *= -1;
                }
                if (facingUp) {
                    localScale.y *= -1;
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                facingUp = false;
                facingLeft = true;
                animator.SetBool("isLooking", false);
                break;
            case 2:
                if (facingLeft) {
                    localScale.x *= -1;
                }
                if (!facingUp) {
                    localScale.y *= -1;
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                facingUp = true;
                facingLeft = false;
                animator.SetBool("isLooking", true);
                break;
            case 3:
                if (!facingLeft) {
                    localScale.x *= -1;
                }
                if (!facingUp) {
                    localScale.y *= -1;
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                facingUp = true;
                facingLeft = true;
                animator.SetBool("isLooking", true);
                break;
            default:
                break;
        }
        transform.localScale = localScale;

        isWaiting = false;
    }
}
