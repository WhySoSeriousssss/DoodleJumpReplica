using UnityEngine;

public class Propeller : Item {

    float duration = 3f;
    public float propulsionForce = 3f;

    bool triggered = false;
    bool running = false;

    Timer timer;

    Rigidbody2D rb2d;

    Animator anim;

    Doodler target;

    public bool Triggered
    {
        get { return triggered; }
    }


    // Use this for initialization
    void Start () {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = duration;
        timer.AddTimerFinishedListener(Drop);

        rb2d = GetComponent<Rigidbody2D>();

        target = FindObjectOfType<Doodler>();

        anim = GetComponent<Animator>();

        events.Add(EventName.PropellerTriggered, new PropellerTriggeredEvent());
        events.Add(EventName.PropellerReleased, new PropellerReleasedEvent());
        EventManager.instance.AddInvoker(EventName.PropellerTriggered, this);
        EventManager.instance.AddInvoker(EventName.PropellerReleased, this);
    }


    private void FixedUpdate()
    {
        if (running)
        {
            rb2d.AddForce(new Vector2(0, propulsionForce), ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler") && !triggered)
        {
            triggered = true;
            running = true;
            timer.Run();
            rb2d.gravityScale = 1;

            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.3f, target.transform.position.z);



            events[EventName.PropellerTriggered].Invoke();
            anim.SetTrigger("triggered");

            EventManager.instance.RemoveInvoker(EventName.PropellerTriggered, this);
            
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private void Drop()
    {
        anim.SetTrigger("finished");
        rb2d.freezeRotation = false;
        running = false;
        rb2d.AddForce(new Vector2(1f, -10f));
        events[EventName.PropellerReleased].Invoke();
        EventManager.instance.RemoveInvoker(EventName.PropellerReleased, this);

    }
}
