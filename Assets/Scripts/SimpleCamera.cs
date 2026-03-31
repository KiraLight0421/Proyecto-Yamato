using UnityEngine;
using Cinemachine;

public class SimpleCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset = new Vector2(2f, 1f);
    [SerializeField] private float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null || virtualCamera == null) return;

        Vector3 targetPos = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            virtualCamera.transform.position.z
        );

        virtualCamera.transform.position = Vector3.Lerp(
            virtualCamera.transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}