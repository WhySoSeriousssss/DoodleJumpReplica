using UnityEngine;

public class Jetpack : Item {

    float duration = 3f;
    public float propulsionForce = 5f;

    bool triggered = false;
    bool running = false;

    bool faceRight = false;

    Timer timer;

    Rigidbody2D rb2d;
    SpriteRenderer sr;

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
        sr = GetComponent<SpriteRenderer>();

        target = FindObjectOfType<Doodler>();

        anim = GetComponent<Animator>();

        events.Add(EventName.JetpackTriggered, new JetPackTriggeredEvent());
        events.Add(EventName.JetpackReleased, new JetpackReleasedEvent());
        EventManager.instance.AddInvoker(EventName.JetpackTriggered, this);
        EventManager.instance.AddInvoker(EventName.JetpackReleased, this);
    }

    private void FixedUpdate()
    {
        if (running)
        {
            Flip();
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
            
            if (!target.FaceRight)
                transform.position = new Vector3(target.transform.position.x + 0.35f, target.transform.position.y + 0.15f, target.transform.position.z);
            else
                transform.position = new Vector3(target.transform.position.x - 0.35f, target.transform.position.y + 0.15f, target.transform.position.z);



            events[EventName.JetpackTriggered].Invoke();
            anim.SetTrigger("triggered");

            EventManager.instance.RemoveInvoker(EventName.JetpackTriggered, this);
            Destroy(GetComponent<BoxCollider2D>());
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
                transform.position = new Vector3(target.transform.position.x + 0.35f, target.transform.position.y + 0.1f, target.transform.position.z);
            else
                transform.position = new Vector3(target.transform.position.x - 0.35f, target.transform.position.y + 0.1f, target.transform.position.z);

            faceRight = !faceRight;
        }
    }

    private void Drop()
    {
        anim.SetTrigger("outOfFuel");
        rb2d.freezeRotation = false;
        running = false;
        rb2d.AddForce(new Vector2(1f, -10f));
        events[EventName.JetpackReleased].Invoke();
        EventManager.instance.RemoveInvoker(EventName.JetpackReleased, this);

    }
}
