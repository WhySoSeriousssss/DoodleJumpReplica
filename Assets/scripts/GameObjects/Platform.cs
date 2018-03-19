using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
