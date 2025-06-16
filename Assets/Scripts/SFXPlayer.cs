using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    //인스펙터에 순서대로 키와 클립 넣고 play함수로 재생시킴
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
