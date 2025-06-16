using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    //�ν����Ϳ� ������� Ű�� Ŭ�� �ְ� play�Լ��� �����Ŵ
    [SerializeField] List<AudioClip> clipList;
    [SerializeField] List<string> keys;
    private Dictionary<string, AudioClip> sfxMap;
    private AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        sfxMap = new Dictionary<string, AudioClip>();
        for (int i = 0; i < keys.Count; i++)
            sfxMap[keys[i]] = clipList[i];
    }

    public void Play(string key)
    {
        if (sfxMap.TryGetValue(key, out var clip))
            src.PlayOneShot(clip);
    }
}
