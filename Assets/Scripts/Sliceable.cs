using System.Collections.Generic;
using UnityEngine;

public class ManualSliceable : MonoBehaviour
{
    [Header("�߶��� ���� Prefabs")]
    public GameObject upperPrefab;
    public GameObject lowerPrefab;
    [Header("���� �и� ���޽� ����")]
    public float sliceForce = 3f;

    private SFXPlayer sfx;

    // Į(Collider)�� ���� ��ġ ����
    private Dictionary<Collider, Vector3> entryPoints = new Dictionary<Collider, Vector3>();

    void Start()
    {
        sfx = FindObjectOfType<SFXPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Knife")) return;
        // ������ ���� Į�� ���� ��ġ�� ����
        entryPoints[other] = other.transform.position;
        sfx.Play("Knife");
        Debug.Log("1");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Knife")) return;
        // entryPoints�� ��ϵ� Į���� Ȯ��
        if (!entryPoints.TryGetValue(other, out Vector3 entryPos))
            return;

        // �������� ���� ��ġ
        Vector3 exitPos = other.transform.position;
        // ���ܸ� ��� ���
        Vector3 planeNormal = (exitPos - entryPos).normalized;

        // 1) ������ ����
        GameObject upper = Instantiate(upperPrefab, transform.position, transform.rotation);
        GameObject lower = Instantiate(lowerPrefab, transform.position, transform.rotation);

        // 2) Rigidbody �߰�/�����ͼ� �� �ֱ�
        Rigidbody rbU = upper.GetComponent<Rigidbody>() ?? upper.AddComponent<Rigidbody>();
        Rigidbody rbL = lower.GetComponent<Rigidbody>() ?? lower.AddComponent<Rigidbody>();
        rbU.AddForce(planeNormal * sliceForce, ForceMode.Impulse);
        rbL.AddForce(-planeNormal * sliceForce, ForceMode.Impulse);

        // 3) ���� ���� ���� & ��� ����
        entryPoints.Remove(other);
        Destroy(gameObject);
    }
}
