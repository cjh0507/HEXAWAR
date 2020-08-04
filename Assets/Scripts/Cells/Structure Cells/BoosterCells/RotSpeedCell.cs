using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코어를 빠르게 회전하게 해주는 셀
public class RotSpeedCell : BoosterCell
{
    public float delta = 0.4f;

    void Start()
    {
        cellType = "RotSpeedCell";
    }
    public override void UpgradeCoreStatus() {
        coreCell.rotSpeed += delta;
    }
    
    
    public override void CanCelEffects() {
        coreCell.rotSpeed -= delta;
    }

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected override void ResetStatus() {

        // mass = 0.2f;
        // maxDurability = 50;
        // if (durability > maxDurability)
        //     durability = maxDurability;
        base.ResetStatus();
        delta = 0.4f;
    }
}
