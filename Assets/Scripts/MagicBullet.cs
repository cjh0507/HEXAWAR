using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 활성화할 시에 총알에 유도 기능 추가
public class MagicBullet : MonoBehaviour
{
    Bullet bullet;
    public float magicRate = 1.0f;


    void Awake() {
        bullet = GetComponent<Bullet>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
