using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 총알의 충돌 이벤트와 제거 관리하는 스크립트
public class Bullet : MonoBehaviour
{
    public float popTime;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", popTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // 총알이 어딘가에 닿았을 때 호출되는 함수
    void OnTriggerEnter2D(Collider2D other)
    {   
        
    }
}
