using UnityEngine;

public class Jetpack : Item {

    public float acceTime = 0.5f;
    public float duration = 2.5f;
    public float runningVelocity = 3f;
    float acceForce;
    float runningForce;
    float doodlerMass = 0.1f;

    bool triggered = false;
    bool accelerating = false;
    bool running = false;

    bool faceRight = false;

    Timer acceTimer;
    Timer runningTimer;

    Rigidbody2D rb2d;

    Animator anim;

    Doodler target;

    Vector2 anchor = new Vector2(-0.13f, -0.85f);

    /*
    public bool Triggered
    {
        get { return triggered; }
    }
    */

    private void Awake()
    {
        height = 0.74f;
        width = 0.48f;
        pivotX = 0.8f;
        pivotY = 0.98f;
        acceForce = doodlerMass * (Physics2D.gravity.magnitude + runningVelocity / acceTime);
        runningForce = doodlerMass * Physics2D.gravity.magnitude;
        connectedAnchor = new Vector2(0.13f, 0.85f);
    }

    override protected void Start()
    {
        base.Start();
        acceTimer = gameObject.AddComponent<Timer>();
        acceTimer.Duration = acceTime;
        acceTimer.AddTimerFinishedListener(FinishAccelerating);

        runningTimer = gameObject.AddComponent<Timer>();
        runningTimer.Duration = duration;
        runningTimer.AddTimerFinishedListener(Drop);

        rb2d = GetComponent<Rigidbody2D>();

        target = FindObjectOfType<Doodler>();

        anim = GetComponent<Animator>();

//        events.Add(EventName.JetpackTriggered, new JetPackTriggeredEvent());
        events.Add(EventName.JetpackReleased, new JetpackReleasedEvent());
//        EventManager.instance.AddInvoker(EventName.JetpackTriggered, this);
        EventManager.instance.AddInvoker(EventName.JetpackReleased, this);
    }

    private void FixedUpdate()
    {
        if (accelerating)
        {
            Flip();
            rb2d.AddForce(new Vector2(0, acceForce), ForceMode2D.Force);
        }
        else if (running)
        {
            Flip();
            rb2d.AddForce(new Vector2(0, runningForce), ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler") && !triggered
            && !collider.gameObject.GetComponent<Doodler>().IsEquippedWithItem)
        {
            DetachPlatform();
            triggered = true;
            accelerating = true;
            acceTimer.Run();
            
            if (!target.FaceRight)
                transform.position = new Vector3(target.transform.position.x + 0.35f, target.transform.position.y + 0.15f, target.transform.position.z);
            else
                transform.position = new Vector3(target.transform.position.x - 0.35f, target.transform.position.y + 0.15f, target.transform.position.z);

//            events[EventName.JetpackTriggered].Invoke();
            anim.SetTrigger("triggered");

//            EventManager.instance.RemoveInvoker(EventName.JetpackTriggered, this);
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    

    private void Flip()
    {
        if (faceRight != target.FaceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            if (faceRight)
                transform.position = new Vector3(target.transform.position.x + 0.35f, target.transform.position.y + 0.2f, target.transform.position.z);
            else
                transform.position = new Vector3(target.transform.position.x - 0.35f, target.transform.position.y + 0.2f, target.transform.position.z);

            faceRight = !faceRight;
        }
    }

    private void FinishAccelerating()
    {
        accelerating = false;
        runningTimer.Run();
        running = true;
    }

    private void Drop()
    {
        anim.SetTrigger("outOfFuel");
        rb2d.freezeRotation = false;
        running = false;
        rb2d.gravityScale = 1;
        rb2d.AddForce(new Vector2(1f, -1f));
        events[EventName.JetpackReleased].Invoke();
        EventManager.instance.RemoveInvoker(EventName.JetpackReleased, this);
    }
}
