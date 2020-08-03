using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCell : BoosterCell
{
    public override void UpgradeCoreStatus() {
        SetQuantity(2f);
        coreCell.speed += quantity;
    }
}
