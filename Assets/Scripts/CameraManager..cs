using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float targetAspectRatio = 16f / 9f;  // 16:9
    [SerializeField] private float baseOrthographicSize = 6.67f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspectRatio / targetAspectRatio;

        if (scaleHeight < 1f)
        {
            // Pantalla más estrecha, aumenta el zoom
            cam.orthographicSize = baseOrthographicSize / scaleHeight;
        }
        else
        {
            // Pantalla más ancha, mantén el tamaño
            cam.orthographicSize = baseOrthographicSize;
        }
    }
}