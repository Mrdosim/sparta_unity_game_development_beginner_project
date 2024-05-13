using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTransform; // 카메라의 Transform
    public float lerpSpeed = 5.0f; // 카메라가 따라잡는 속도
    private Vector3 offset; // 초기 오프셋

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // 메인 카메라 자동 할당
        }

        // 플레이어와 카메라 사이의 초기 오프셋 계산
        offset = cameraTransform.position - transform.position;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        // 플레이어의 현재 위치에 오프셋을 더하여 카메라가 이동할 목표 위치를 계산
        Vector3 targetPosition = transform.position + offset;

        // 카메라 위치를 부드럽게 이동
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
