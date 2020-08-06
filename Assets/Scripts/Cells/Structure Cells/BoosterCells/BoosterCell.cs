using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterCell : Cell
{
    public abstract void UpgradeCoreStatus();

    public abstract void CanCelEffects();

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected override void ResetStatus() {
        base.ResetStatus();
    }

}
