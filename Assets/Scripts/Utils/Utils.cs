using UnityEngine;
using UnityEngine.Rendering;

public class Utils : MonoBehaviour
{
    public static float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static (float, float) GetCameraBounds()
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        
        return (cameraWidth, cameraHeight);
    }
}
