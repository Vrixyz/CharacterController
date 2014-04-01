using UnityEngine;
using System.Collections;

public static class Helper {

    static public float CameraClamp(float val, float min, float max)
    {
        return (Mathf.Clamp(val, min, max));
    }
}
