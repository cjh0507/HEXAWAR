using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterCell : Cell
{
    public abstract void UpgradeCoreStatus();

    public abstract void CanCelEffects();

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected override void ResetStatus() {

        // mass = 0.2f;
        // maxDurability = 50;
        // if (durability > maxDurability)
        //     durability = maxDurability;
        base.ResetStatus();
    }

}
