using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSpeedCell : BoosterCell
{
    public override void UpgradeCoreStatus() {
        SetQuantity(0.75f);
        coreCell.rotSpeed += quantity;
    }
}
