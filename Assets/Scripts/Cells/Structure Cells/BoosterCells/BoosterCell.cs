using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterCell : Cell
{
    [SerializeField]
    protected float quantity;
    [SerializeField]
    protected CoreCell coreCell;
    public void FindCore() {
        if(isAttached)
            coreCell = transform.parent.gameObject.GetComponent<CoreCell>();
    }

    protected void SetQuantity(float num) {
        quantity = num;
    }
    public abstract void UpgradeCoreStatus();

}
