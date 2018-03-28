using UnityEngine;

public class Propeller : Item {

    public float acceTime = 0.5f;
    public float duration = 2.5f;
    public float runningVelocity = 3f;
    float acceForce;
    float runningForce;
    float doodlerMass = 0.1f;

    bool triggered = false;
    bool accelerating = false;
    bool running = false;

    Timer acceTimer;
    Timer runningTimer;

    Rigidbody2D rb2d;

    Animator anim;

    Doodler target;


    /*
    public bool Triggered
    {
        get { return triggered; }
    }

    public bool IsRunning
    {
        get { return running; } 
    }
    */


    private void Awake()
    {
        height = 0.4f;
        width = 0.61f;
        pivotX = 0.5f;
        pivotY = 0.5f;
        acceForce = doodlerMass * (Physics2D.gravity.magnitude + runningVelocity / acceTime);
        runningForce = doodlerMass * Physics2D.gravity.magnitude;
        connectedAnchor = new Vector2(0, 0.28f);
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

//        events.Add(EventName.PropellerTriggered, new PropellerTriggeredEvent());
        events.Add(EventName.PropellerReleased, new PropellerReleasedEvent());
//        EventManager.instance.AddInvoker(EventName.PropellerTriggered, this);
        EventManager.instance.AddInvoker(EventName.PropellerReleased, this);
    }


    private void FixedUpdate()
    {
        if (accelerating)
        {
            rb2d.AddForce(new Vector2(0, acceForce), ForceMode2D.Force);
        }
        else if (running)
        {
            rb2d.AddForce(new Vector2(0, runningForce), ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler") && !triggered 
            && !target.IsEquippedWithItem)
        {
            DetachPlatform();
            triggered = true;
            accelerating = true;
            acceTimer.Run();

            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);

//            events[EventName.PropellerTriggered].Invoke();
            anim.SetTrigger("triggered");

//            EventManager.instance.RemoveInvoker(EventName.PropellerTriggered, this);
            GetComponent<BoxCollider2D>().enabled = false;

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
        anim.SetTrigger("finished");
        rb2d.freezeRotation = false;
        running = false;
        rb2d.gravityScale = 1;
        rb2d.AddForce(new Vector2(1f, -1f));
        events[EventName.PropellerReleased].Invoke();
        EventManager.instance.RemoveInvoker(EventName.PropellerReleased, this);
    }
}
