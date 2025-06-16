using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    private int juiceType;//0-2
    [SerializeField] GameObject bubble;//말풍선 이미지
    [SerializeField] Image[] juiceImage;//쥬스 이미지
    [SerializeField] Animator animator;
    [SerializeField] SkinnedMeshRenderer faceRenderer;

    int happyIndex, angryIndex;
    private void Awake()
    {
        var mesh = faceRenderer.sharedMesh;
        happyIndex = mesh.GetBlendShapeIndex("blendShape1.Joy");
        angryIndex = mesh.GetBlendShapeIndex("blendShape1.Angry");
    }
    private void OnEnable()
    {
        juiceType = Random.Range(0, 3);
        juiceImage[juiceType].gameObject.SetActive(true);

        if (happyIndex >= 0) faceRenderer.SetBlendShapeWeight(happyIndex, 0f);
        if (angryIndex >= 0) faceRenderer.SetBlendShapeWeight(angryIndex, 0f);

        animator.SetInteger("IsCorrect", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Juice")) return;

        var j = other.GetComponent<Juice>();
        if (j == null) return;

        bool correct = (j.type == juiceType);

        // 애니메이터 파라미터에 1=웃음, 2=화남
        animator.SetInteger("IsCorrect", correct ? 1 : 2);

        faceRenderer.SetBlendShapeWeight(happyIndex, correct ? 100f : 0f);
        faceRenderer.SetBlendShapeWeight(angryIndex, correct ? 0f : 100f);

        // 쥬스 말풍선 제거 
        Destroy(other.gameObject);
        bubble.gameObject.SetActive(false);
    }

}
