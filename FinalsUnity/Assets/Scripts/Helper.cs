using UnityEngine;
using System.Collections;

public static class Helper {

    public struct ClipPlaneStruct
    {
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerLeft;
        public Vector3 LowerRight;
    };

    static public float CameraClamp(float val, float min, float max)
    {
        return (Mathf.Clamp(val, min, max));
    }

    static public ClipPlaneStruct FindNearClipPlanePositions()
    {
        ClipPlaneStruct cpp = new ClipPlaneStruct();

        cpp.LowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        cpp.LowerRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        cpp.UpperLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        cpp.UpperRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        return (cpp);
    }
}
