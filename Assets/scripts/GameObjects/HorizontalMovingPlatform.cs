using UnityEngine;

public class HorizontalMovingPlatform : Platform {

    public float movingVelocity;

    float screenLeft;
    float screenRight;

    float colliderHalfWidth;
    int direction = -1;

 //   FixedJoint2D fj;

	// Use this for initialization
	void Start () {
        screenLeft = ScreenUtils.ScreenLeft;
        screenRight = ScreenUtils.ScreenRight;

        Vector2 []points = GetComponent<EdgeCollider2D>().points;
        colliderHalfWidth = (points[1].x - points[0].x) / 2;

 //       fj.GetComponent<FixedJoint2D>(); why there is a null reference exception with this line?
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

    public override void AttachItem(GameObject itemPrefab)
    {
        GameObject item = Instantiate(itemPrefab);

        Item itemScript = item.GetComponent<Item>();
        if (itemScript != null)
        {
            float itemWidth = itemScript.Width;
            float itemHeight = itemScript.Height;
            float itemPivotX = itemScript.PivotX;
            float itemPivotY = itemScript.PivotY;
            float platformHalfHeight = 0.15f;

            float newX = transform.position.x + itemWidth * (itemPivotX - 0.5f);
            float newY = transform.position.y + platformHalfHeight + itemHeight * itemPivotY - 0.08f;
            Vector3 itemPos = new Vector3(newX, newY, transform.position.z);
            item.transform.position = itemPos;

            itemScript.PlatformJoint(gameObject);
            /*
            fj = GetComponent<FixedJoint2D>();
            fj.enabled = true;
            fj.connectedBody = item.GetComponent<Rigidbody2D>();
            fj.connectedAnchor = new Vector2(-0.02f, -0.27f);
            */
        }
    }

    /*
    private void DetachItem()
    {
        fj = GetComponent<FixedJoint2D>();
        fj.enabled = false;
        fj.connectedBody = null;
    }
    */
}
