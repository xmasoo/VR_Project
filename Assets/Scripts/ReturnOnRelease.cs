using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class ReturnOnRelease : MonoBehaviour
{
    [Header("���ư� �� �ε巴�� �̵��� �ð� (��)")]
    [SerializeField] private float returnDuration = 0.3f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    // �����ص� �ʱ� ��ġ/ȸ��
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // ���� ���� ��ġ��ȸ�� ����
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // ���� �̺�Ʈ ������ ���
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // ���� ���� ����ġ��
        StopAllCoroutines();
        StartCoroutine(ReturnRoutine());
    }

    private IEnumerator ReturnRoutine()
    {
        // ���� ���� ����
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

        // ���� ����ġ
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // ���� �ٽ� �ѱ�
        rb.isKinematic = false;
    }
}
