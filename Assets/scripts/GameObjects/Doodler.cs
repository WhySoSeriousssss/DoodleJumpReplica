using UnityEngine;

public class Doodler : MonoBehaviour {

    public float normalJumpForce = 40f;
    public float springJumpForce = 70f;

    public float horizontalVelocity = 10f;

    float movement;

    Rigidbody2D rb;
    SpriteRenderer sr;
    
    bool faceRight = true;

    public Sprite normalSprite;
    public Sprite jumpSprite;

    bool isEquippedWithItem = false;

    // Use this for initialization
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        EventManager.instance.AddListener(EventName.SpringTriggered, JumpOnSpring);
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
                FlipFacingDirectionSprite();
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
                Jump(normalJumpForce);
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
                Jump(normalJumpForce);
            }
            else
            {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }


    private void OnBecameInvisible()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < 0)
        {
            Destroy(gameObject);
            MenuManager.GoToMenu(MenuName.Gameover);
        }
    }


    private void Jump(float jumpForce)
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void JumpOnSpring()
    {
        Jump(springJumpForce);
    }

    private void EquipJetPack()
    {
        if (!isEquippedWithItem)
        {
            isEquippedWithItem = true;

        }
    }

    private void FlipFacingDirectionSprite()
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
