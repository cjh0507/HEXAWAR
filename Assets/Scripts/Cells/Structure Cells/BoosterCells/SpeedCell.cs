using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코어를 빠르게 만들어주는 셀
public class SpeedCell : BoosterCell
{
    public float speedDelta = 1f;
    public float accelDelta = 2.5f;

    void Start()
    {
        cellType = "SpeedCell";
    }
    public override void UpgradeCoreStatus() {
        // Debug.Log($"UpgradeStatus() Called - speedDelta = {speedDelta}, accelDelta = {accelDelta}");
        coreCell.speed += speedDelta;
        coreCell.acceleration += accelDelta;
    }

    public override void CanCelEffects() {
        // Debug.Log($"CancelEffects() called - speedDelta = {speedDelta}, accelDelta = {accelDelta}");
        coreCell.speed -= speedDelta;
        coreCell.acceleration -= accelDelta;
    }

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected override void ResetStatus() {

        // mass = 0.2f;
        // maxDurability = 50;
        // if (durability > maxDurability)
        //     durability = maxDurability;
        base.ResetStatus();
        speedDelta = 1f;
        accelDelta = 2.5f;
    }
}
