using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenUtils {
    public static float ScreenLeft { get; private set; }
    public static float ScreenRight { get; private set; }
    public static float ScreenTop { get; private set; }
    public static float ScreenBottom { get; private set; }

    public static void Initialize() {
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            Screen.width, Screen.height, screenZ);
        Vector3 lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        ScreenLeft = lowerLeftCornerWorld.x;
        ScreenRight = upperRightCornerWorld.x;
        ScreenTop = upperRightCornerWorld.y;
        ScreenBottom = lowerLeftCornerWorld.y;
    }
}
