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
        /*
        damage *= 2;
        range *= 2;
        coolTime *= 3;
        shotSpeed *= 2;
        bulletSpread = 0;
        */
    }

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected override void ResetStatus() {

        // mass = 0.2f;
        // maxDurability = 50;
        // if (durability > maxDurability)
        //     durability = maxDurability;
        base.ResetStatus();

        haveGrow = 0;
        haveMagic = 0;
        damage = 5f; 
        coolTime = 1.5f; 
        range = 12;
        shotSpeed = 10;
    }

}
