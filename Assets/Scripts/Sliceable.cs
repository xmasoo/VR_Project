using System.Collections.Generic;
using UnityEngine;

public class ManualSliceable : MonoBehaviour
{
    [Header("잘라진 반쪽 Prefabs")]
    public GameObject upperPrefab;
    public GameObject lowerPrefab;
    [Header("조각 분리 임펄스 세기")]
    public float sliceForce = 3f;

    private SFXPlayer sfx;

    // 칼(Collider)별 진입 위치 저장
    private Dictionary<Collider, Vector3> entryPoints = new Dictionary<Collider, Vector3>();

    void Start()
    {
        sfx = FindObjectOfType<SFXPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Knife")) return;
        // 진입한 순간 칼의 월드 위치를 저장
        entryPoints[other] = other.transform.position;
        sfx.Play("Knife");
        Debug.Log("1");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Knife")) return;
        // entryPoints에 기록된 칼인지 확인
        if (!entryPoints.TryGetValue(other, out Vector3 entryPos))
            return;

        // 빠져나간 순간 위치
        Vector3 exitPos = other.transform.position;
        // 절단면 노멀 계산
        Vector3 planeNormal = (exitPos - entryPos).normalized;

        // 1) 프리팹 생성
        GameObject upper = Instantiate(upperPrefab, transform.position, transform.rotation);
        GameObject lower = Instantiate(lowerPrefab, transform.position, transform.rotation);

        // 2) Rigidbody 추가/가져와서 힘 주기
        Rigidbody rbU = upper.GetComponent<Rigidbody>() ?? upper.AddComponent<Rigidbody>();
        Rigidbody rbL = lower.GetComponent<Rigidbody>() ?? lower.AddComponent<Rigidbody>();
        rbU.AddForce(planeNormal * sliceForce, ForceMode.Impulse);
        rbL.AddForce(-planeNormal * sliceForce, ForceMode.Impulse);

        // 3) 원본 과일 제거 & 기록 삭제
        entryPoints.Remove(other);
        Destroy(gameObject);
    }
}
