using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    [Tooltip("과일 오브젝트에 붙인 Tag (예: \"Fruit\")")]
    public string fruitTag = "Fruit";
    [Tooltip("스폰할 쥬스 Prefab")]
    public GameObject juicePrefab;
    [Tooltip("쥬스를 생성할 위치 (빈 Transform)")]
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        // 과일 태그 검사
        if (!other.CompareTag(fruitTag)) return;
        Debug.Log("태그 과일임");
        // 쥬스 스폰
        if (juicePrefab != null && spawnPoint != null)
        {
            Instantiate(juicePrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("쥬스 생성됨");
        }
        else
        {
            Debug.LogWarning("SimpleBlender: juicePrefab 또는 spawnPoint가 할당되지 않았습니다.");
        }

        // 원본 과일 제거(원하면 주석 처리)
        Destroy(other.gameObject);
    }
}
