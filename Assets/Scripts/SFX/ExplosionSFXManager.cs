using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSFXManager : MonoBehaviour
{
    public AudioClip explosionSFX;
    AudioSource myAudio; // 컴퍼넌트에서 AudioSource가져오기
    public static ExplosionSFXManager instance; // 다른 스크립트에서 이스크립트에있는 함수를 호출할때 쓰임 (Singleton)

    public float lowPitchRange = .90f;				//The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.1f;			//The highest a sound effect will be randomly pitched.

    void Awake()  // Start함수보다 먼저 호출됨
    { 
        if (ExplosionSFXManager.instance == null)  // 게임시작했을때 이 instance가 없을때
            ExplosionSFXManager.instance = this;  // instance를 생성
    }
    // Use this for initialization
    void Start () { 
        myAudio = GetComponent<AudioSource>();  // myAudio에 컴퍼넌트에있는 AudioSource넣기
    } 
    public void PlayExplosionSound() 
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			
			//Set the pitch of the audio source to the randomly chosen pitch.
		myAudio.pitch = randomPitch;
        myAudio.PlayOneShot(explosionSFX);
    }
}
