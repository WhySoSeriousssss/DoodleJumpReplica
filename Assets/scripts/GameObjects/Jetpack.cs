using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Item {

	// Use this for initialization
	void Start () {
        events.Add(EventName.JetpackTriggered, new JetPackTriggeredEvent());
        EventManager.instance.AddInvoker(EventName.JetpackTriggered, this);
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Doodler"))
        {

        }
    }

}
