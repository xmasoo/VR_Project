using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    [Tooltip("���� ������Ʈ�� ���� Tag (��: \"Fruit\")")]
    public string fruitTag = "Fruit";
    [Tooltip("������ �꽺 Prefab")]
    public GameObject juicePrefab;
    [Tooltip("�꽺�� ������ ��ġ (�� Transform)")]
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        // ���� �±� �˻�
        if (!other.CompareTag(fruitTag)) return;
        Debug.Log("�±� ������");
        // �꽺 ����
        if (juicePrefab != null && spawnPoint != null)
        {
            Instantiate(juicePrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("�꽺 ������");
        }
        else
        {
            Debug.LogWarning("SimpleBlender: juicePrefab �Ǵ� spawnPoint�� �Ҵ���� �ʾҽ��ϴ�.");
        }

        // ���� ���� ����(���ϸ� �ּ� ó��)
        Destroy(other.gameObject);
    }
}
