using UnityEngine;

public class ScreenWrapper : MonoBehaviour {

    private void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        if (position.x > ScreenUtils.ScreenRight 
            || position.x < ScreenUtils.ScreenLeft)
        {
            position.x *= -1;
        }

        transform.position = position;
    }
}
