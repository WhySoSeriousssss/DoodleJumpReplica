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

    bool shielded = false;

    DistanceJoint2D dj;


    public bool FaceRight
    {
        get { return faceRight; }
    }



    // Use this for initialization
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        dj = gameObject.GetComponent<DistanceJoint2D>();

        EventManager.instance.AddListener(EventName.SpringTriggered, JumpOnSpring);
        EventManager.instance.AddListener(EventName.JetpackTriggered, EquipJetPack);
        EventManager.instance.AddListener(EventName.JetpackReleased, ReleaseItem);
        EventManager.instance.AddListener(EventName.PropellerTriggered, EquipPropeller);
        EventManager.instance.AddListener(EventName.PropellerReleased, ReleaseItem);
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
            if (collision.relativeVelocity.y >= -0.0001)
            {
                Jump(normalJumpForce);
            }
        }      
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isEquippedWithItem)
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
    }


    private void OnBecameInvisible()
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.y < 0)
            {
                Destroy(gameObject);
                MenuManager.GoToMenu(MenuName.Gameover);
            }
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
            Jetpack jetpack = null;
            foreach (Jetpack jp in FindObjectsOfType<Jetpack>())
            {
                if (jp.Triggered)
                    jetpack = jp;
            }
            if (jetpack != null)
            {
                dj.enabled = true;
                dj.connectedBody = jetpack.GetComponent<Rigidbody2D>();
            }
        }
    }


    private void EquipPropeller()
    {
        if (!isEquippedWithItem)
        {
            isEquippedWithItem = true;
            Propeller propeller = null;
            foreach (Propeller prop in FindObjectsOfType<Propeller>())
            {
                if (prop.Triggered)
                    propeller = prop;
            }
            if (propeller != null)
            {
                dj.enabled = true;
                dj.connectedBody = propeller.GetComponent<Rigidbody2D>();
            }
        }
    }


    private void ReleaseItem()
    {
        isEquippedWithItem = false;
        dj.enabled = false;
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
