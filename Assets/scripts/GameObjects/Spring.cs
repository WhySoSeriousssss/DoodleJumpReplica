using UnityEngine;

public class Spring : Item {

    public Sprite spring_up;

	// Use this for initialization
	void Start () {
        events.Add(EventName.SpringTriggered, new SpringTriggeredEvent());
        EventManager.instance.AddInvoker(EventName.SpringTriggered, this);     
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler"))
        {
            if (collider.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                events[EventName.SpringTriggered].Invoke();
                GetComponent<SpriteRenderer>().sprite = spring_up;
                EventManager.instance.RemoveInvoker(EventName.SpringTriggered, this);
            }
            
        }
    }

}
