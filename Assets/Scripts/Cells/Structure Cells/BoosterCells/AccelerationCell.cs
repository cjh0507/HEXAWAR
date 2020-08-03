using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationCell : BoosterCell
{
    public override void UpgradeCoreStatus() {
        SetQuantity(2.5f);
        coreCell.acceleration += quantity;
    }
}
