using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSFXManager : MonoBehaviour
{
    public AudioClip pauseSFX;
    AudioSource myAudio; // 컴퍼넌트에서 AudioSource가져오기
    public static PauseSFXManager instance; // 다른 스크립트에서 이스크립트에있는 함수를 호출할때 쓰임 (Singleton)

    void Awake()  // Start함수보다 먼저 호출됨
    { 
        if (PauseSFXManager.instance == null)  // 게임시작했을때 이 instance가 없을때
            PauseSFXManager.instance = this;  // instance를 생성
    }
    // Use this for initialization
    void Start () { 
        myAudio = GetComponent<AudioSource>();  // myAudio에 컴퍼넌트에있는 AudioSource넣기
    } 
    public void PlayPauseSound() 
    {
        myAudio.PlayOneShot(pauseSFX);
    }
}
