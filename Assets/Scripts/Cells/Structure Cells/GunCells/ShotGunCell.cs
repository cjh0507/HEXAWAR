using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사거리 절반, 딜레이 2배(공격속도 절반)
// 가까이에 명중률 낮은 4발 쏨
public class ShotGunCell : GunCell
{
    private Quaternion[] rots = {
        Quaternion.Euler(new Vector3(0, 0, 5)), Quaternion.Euler(new Vector3(0, 0, -5)),
        Quaternion.Euler(new Vector3(0, 0, 15)), Quaternion.Euler(new Vector3(0, 0, -15))
    };

    
    protected override void Awake() {
        base.Awake();
        range *= 0.5f;
        coolTime *= 2;
    }

    // (마우스 커서 방향으로) 총알을 발사한다
    protected override void FireAutomatically() {
        Vector2 dir = GetNormalVector();
        // 마우스 커서 방향 얻어오기
        // Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - transform.position;
        dir = SetBulletSpread(dir);

        float radian5 = (float) (Mathf.PI * 5 / 180.0);
        float radian15 = 3 * radian5;
        
        // 이웃하는 총알의 궤적이 이루는 각이 10도인 총알 4발 발사
        for(int i = 0; i < rots.Length; i++) {
            MakeBullet(rots[i]* dir);
        }
        // 쿨타임 시작
        StartCoroutine(WaitForCoolTime());
    }

}
