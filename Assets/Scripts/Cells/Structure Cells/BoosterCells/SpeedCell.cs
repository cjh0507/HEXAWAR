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
        coreCell.speed += speedDelta;
        coreCell.acceleration += accelDelta;
    }

    public override void CanCelEffects() {
        coreCell.speed -= speedDelta;
        coreCell.acceleration -= accelDelta;
    }
}
