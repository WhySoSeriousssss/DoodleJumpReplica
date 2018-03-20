using UnityEngine;

public class Item : EmptyEventInvoker {

	// Use this for initialization
	void Start () {
		
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
