using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : Platform {

    public float movingVelocity;

    float screenLeft;
    float screenRight;

    float colliderHalfWidth;
    int direction = -1;

	// Use this for initialization
	void Start () {
        screenLeft = ScreenUtils.ScreenLeft;
        screenRight = ScreenUtils.ScreenRight;

        Vector2 []points = GetComponent<EdgeCollider2D>().points;
        colliderHalfWidth = (points[1].x - points[0].x) / 2;
	}

    private void FixedUpdate()
    {
        float x = transform.position.x;
        if (x + colliderHalfWidth >= screenRight)
        {
            direction = -1;
        }
        else if (x - colliderHalfWidth <= screenLeft)
        {
            direction = 1;
        }
        transform.position = new Vector3(x + direction * movingVelocity, transform.position.y, transform.position.z);
    }
}
