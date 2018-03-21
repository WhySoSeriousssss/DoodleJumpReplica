using UnityEngine;

public class Item : EmptyEventInvoker {

	// Use this for initialization
	void Start () {
		
	}

    private void OnBecameInvisible()
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.y < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
