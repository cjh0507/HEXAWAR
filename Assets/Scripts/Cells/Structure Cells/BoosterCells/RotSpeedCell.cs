using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSpeedCell : BoosterCell
{
    public float delta = 0.4f;
    public override void UpgradeCoreStatus() {
        coreCell.rotSpeed += delta;
    }
}
