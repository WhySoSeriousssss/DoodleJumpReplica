using UnityEngine;

public class Platform : MonoBehaviour {
	
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    virtual public void AttachItem(GameObject item)
    {
        Debug.Log("I can put anything i want here because this function will not be called whatsoever. " +
            "So if I am watching this message, then i am in trouble.");
    }
}
