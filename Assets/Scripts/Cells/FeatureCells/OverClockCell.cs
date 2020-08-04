using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접한 셀들의 "출력"을 강하게 만든다
public class OverClockCell : FeatureCell
{
    public override void GiveFeature(int id) {
        Cell targetCell = adjacentCells[id];
        if(targetCell != null && targetCell.cellType != "FeatureCell")
        {
            // CoreCell은 GunCell처럼 취급
            if (targetCell.cellType == "CoreCell") {
                ((CoreCell) targetCell).damage *= 1.3f;
                ((CoreCell) targetCell).range *= 1.3f;
                ((CoreCell) targetCell).shotSpeed *= 1.3f;
            }

            if (targetCell.cellType == "GunCell") {
                ((GunCell) targetCell).damage *= 1.3f;
                ((GunCell) targetCell).range *= 1.3f;
                ((GunCell) targetCell).shotSpeed *= 1.3f;
            }
            if (targetCell.cellType == "SpeedCell") {
                ((SpeedCell) targetCell).CanCelEffects();
                ((SpeedCell) targetCell).speedDelta *= 1.3f;
                ((SpeedCell) targetCell).accelDelta *= 1.3f;
                ((SpeedCell) targetCell).UpgradeCoreStatus();
            }
            if (targetCell.cellType == "RotSpeedCell") {
                ((RotSpeedCell) targetCell).CanCelEffects();
                ((RotSpeedCell) targetCell).delta *= 1.3f;
                ((RotSpeedCell) targetCell).UpgradeCoreStatus();
            }
        }
    }
    
    public override void CancelFeatrue(int id) {

    }

}
