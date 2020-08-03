using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데미지, 사거리, 투사체속도 2배
// 공격 딜레이 3배 (공격속도 1/3)
// 탄퍼짐 없음
public class SniperCell : GunCell
{
    protected override void Awake() {
        base.Awake();
        damage *= 2;
        range *= 2;
        coolTime *= 3;
        shotSpeed *= 2;
        bulletSpread = 0;
    }

}
