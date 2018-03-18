using UnityEngine;

public static class ScreenUtils {

    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    public static float ScreenLeft
    {
        get { return screenLeft; }
    }

    public static float ScreenRight
    {
        get { return screenRight; }
    }

    public static float ScreenTop
    {
        get { return screenTop; }
    }

    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    public static void Initialize()
    {
        Vector3 leftBottomPoint = new Vector3(0, 0, Camera.main.transform.position.z);
        Vector3 rightTopPoint = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);

        Vector3 leftBottomPointWorld = Camera.main.ScreenToWorldPoint(leftBottomPoint);
        Vector3 rightTopPointWorld = Camera.main.ScreenToWorldPoint(rightTopPoint);

        screenLeft = leftBottomPointWorld.x;
        screenRight = rightTopPointWorld.x;
        screenTop = rightTopPointWorld.y;
        screenBottom = leftBottomPointWorld.y;

    }
}
