using UnityEngine;

public class Item : EmptyEventInvoker {

    protected float height;
    protected float width;
    protected float pivotX;
    protected float pivotY;

    FixedJoint2D fj;
    protected Vector2 connectedAnchor;

    public float Height
    {
        get { return height; }
    }

    public float Width
    {
        get { return width; }
    }

    public float PivotX
    {
        get { return pivotX; }
    }

    public float PivotY
    {
        get { return pivotY; }
    }

    virtual protected void Start()
    {
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

    public void PlatformJoint(GameObject platform)
    {
        fj = GetComponent<FixedJoint2D>();
        fj.enabled = true;
        fj.connectedBody = platform.GetComponent<Rigidbody2D>();
        fj.connectedAnchor = connectedAnchor;
    }

    protected void DetachPlatform()
    {
        fj = GetComponent<FixedJoint2D>();
        fj.connectedBody = null;
        fj.enabled = false;
    }
}
