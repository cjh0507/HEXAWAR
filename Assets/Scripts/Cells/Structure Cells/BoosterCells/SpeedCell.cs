using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCell : BoosterCell
{
    public float speedDelta = 1f;
    public float accelDelta = 2.5f;
    public override void UpgradeCoreStatus() {
        coreCell.speed += speedDelta;
        coreCell.acceleration += accelDelta;
    }
}
