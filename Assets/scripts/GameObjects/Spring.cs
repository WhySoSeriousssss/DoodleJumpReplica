using UnityEngine;

public class Spring : Item {

    public Sprite spring_up;

    bool triggered = false;


    private void Awake()
    {
        height = 0.23f;
        width = 0.34f;
        pivotX = 0.5f;
        pivotY = 0.5f;
        connectedAnchor = new Vector2(0, 0.24f);
    }

    override protected void Start () {
        base.Start();
        events.Add(EventName.SpringTriggered, new SpringTriggeredEvent());
        EventManager.instance.AddInvoker(EventName.SpringTriggered, this);     
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler") && !triggered && !collider.isTrigger)
        {
            if (collider.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.001)
            {
                events[EventName.SpringTriggered].Invoke();
                GetComponent<SpriteRenderer>().sprite = spring_up;
                EventManager.instance.RemoveInvoker(EventName.SpringTriggered, this);
                triggered = true;

            }
        }
    }

}
