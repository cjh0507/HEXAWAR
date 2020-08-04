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
}
