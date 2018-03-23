using UnityEngine;

public class Spring : Item {

    public Sprite spring_up;

    bool triggered = false;

	// Use this for initialization
	void Start () {
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
