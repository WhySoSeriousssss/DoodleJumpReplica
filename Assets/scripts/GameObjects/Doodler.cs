using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodler : MonoBehaviour {

    public float verticalForce = 40f;

    public float horizontalVelocity = 10f;

    float movement;

    Rigidbody2D rb;
    SpriteRenderer sr;
    
    bool faceRight = true;

    public Sprite normalSprite;
    public Sprite jumpSprite;

    // Use this for initialization
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
        
        if(movement != 0)
        {
            if ((faceRight && (movement < 0)) || (!faceRight && (movement > 0)))
            {
                faceRight = !faceRight;
                FlipSprite();
            }

            rb.velocity = new Vector2(movement * horizontalVelocity, rb.velocity.y);
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            if (collision.relativeVelocity.y > 0)
            {
                Jump();
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Monster"))
        {
            if (collider.transform.position.y < transform.position.y)
            {
                Destroy(collider.gameObject);
                Jump();
            }
            else
            {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, verticalForce));
    }

    private void FlipSprite()
    {
        if (faceRight)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
}
