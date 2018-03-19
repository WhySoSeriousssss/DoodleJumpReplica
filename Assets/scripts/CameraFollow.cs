using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    // Update is called once per frame
    void LateUpdate () {
        if (target != null)
        {
            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
        }
	}
}
