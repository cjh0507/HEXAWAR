using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데미지 2/3, 딜레이 절반(공격속도 두 배)
// 탄퍼짐이 조금 더 심함, 투사체 속도 조금 더 빠름
public class SubmachineGell : GunCell
{
    protected override void Awake() {
        base.Awake();
        /*
        damage *= 0.67f;
        range *= 0.75f;
        coolTime *= 0.5f;
        shotSpeed *= 1.2f;
        bulletSpread *= 1.5f;
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
        damage = 1.675f; 
        coolTime = 0.25f; 
        range = 4.5f;
        shotSpeed = 6;
    }

}
