using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class ReturnOnRelease : MonoBehaviour
{
    [Header("돌아갈 때 부드럽게 이동할 시간 (초)")]
    [SerializeField] private float returnDuration = 0.3f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    // 저장해둘 초기 위치/회전
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // 시작 시점 위치·회전 저장
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // 해제 이벤트 리스너 등록
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // 해제 직후 원위치로
        StopAllCoroutines();
        StartCoroutine(ReturnRoutine());
    }

    private IEnumerator ReturnRoutine()
    {
        // 물리 끄고 스냅
        rb.isKinematic = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float elapsed = 0f;

        while (elapsed < returnDuration)
        {
            float t = elapsed / returnDuration;
            transform.position = Vector3.Lerp(startPos, initialPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, initialRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종 정위치
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // 물리 다시 켜기
        rb.isKinematic = false;
    }
}
