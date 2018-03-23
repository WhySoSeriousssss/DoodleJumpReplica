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
        EventManager.instance.AddListener(EventName.HitByMonster, Fall);
        EventManager.instance.AddListener(EventName.StepOnMonster, Jump);
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
                Jump();
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


    private void Jump()
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, normalJumpForce));
    }

    private void JumpOnSpring()
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, springJumpForce));
    }



    private void EquipJetPack()
    {
        if (!isEquippedWithItem)
        {
            rb.velocity = new Vector2(0, 0);
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
            rb.velocity = new Vector2(0, 0);
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
        dj.connectedBody = null;
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

    private void Fall()
    {
        rb.AddForce(new Vector2(0, -1f), ForceMode2D.Impulse);
        GetComponents<BoxCollider2D>()[0].enabled = false;
        GetComponents<BoxCollider2D>()[1].enabled = false;
    }
}
