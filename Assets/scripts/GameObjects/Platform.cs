using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
