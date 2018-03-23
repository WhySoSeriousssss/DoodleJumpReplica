using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : EmptyEventInvoker {

    bool hit = false;

	// Use this for initialization
	void Start () {
        events.Add(EventName.HitByMonster, new HitByMonsterEvent());
        events.Add(EventName.StepOnMonster, new StepOnMonsterEvent());
        EventManager.instance.AddInvoker(EventName.HitByMonster, this);
        EventManager.instance.AddInvoker(EventName.StepOnMonster, this);
	}


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!hit && collider.gameObject.CompareTag("Doodler"))
        {
            float halfColliderHeight = (collider as BoxCollider2D).size.y / 2;
            if (transform.position.y + halfColliderHeight <= collider.transform.position.y + 0.01
                && collider.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.001)
            {
                Debug.Log("step");
                events[EventName.StepOnMonster].Invoke();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("hit");
                events[EventName.HitByMonster].Invoke();
            }
            hit = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveInvoker(EventName.HitByMonster, this);
        EventManager.instance.RemoveInvoker(EventName.StepOnMonster, this);
    }

}
