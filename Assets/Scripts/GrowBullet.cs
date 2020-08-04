using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 활성화시에 시간이 지남에 따라(정확히는 사거리 끝에 갈수록) 점점 커지고 데미지가 세지는 총알이 된다.
public class GrowBullet : MonoBehaviour
{
    Bullet bullet;
    public float growRate = 1.0f;
    float deltaPerUpdate;

    void Awake() {
        bullet = GetComponent<Bullet>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // 사거리 끝에 도달하면 3배가 된다
        deltaPerUpdate = (Time.fixedDeltaTime /  bullet.popTime) * bullet.damage * 2.5f; 

        // 처음엔 0.5배
        bullet.damage *= 0.5f;

        bullet.ScaleByDamage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bullet.damage += deltaPerUpdate * growRate;
        bullet.ScaleByDamage();
    }
}
