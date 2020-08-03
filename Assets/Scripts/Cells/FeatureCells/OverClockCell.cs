using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접한 셀들의 "출력"을 강하게 만든다
public class OverClockCell : FeatureCell
{
    public override void GiveFeature() {
        foreach (Cell cell in adjacentCells) {
            if (cell == null || cell.cellType == "FeatureCell")
                continue;

            // CoreCell은 GunCell처럼 취급
            if (cell.cellType == "CoreCell") {
                ((CoreCell) cell).damage *= 1.3f;
                ((CoreCell) cell).range *= 1.3f;
                ((CoreCell) cell).shotSpeed *= 1.3f;
            }

            if (cell.cellType == "GunCell") {
                ((GunCell) cell).damage *= 1.3f;
                ((GunCell) cell).range *= 1.3f;
                ((GunCell) cell).shotSpeed *= 1.3f;
            }
            if (cell.cellType == "SpeedCell") {
                ((SpeedCell) cell).CanCelEffects();
                ((SpeedCell) cell).speedDelta *= 1.3f;
                ((SpeedCell) cell).accelDelta *= 1.3f;
                ((SpeedCell) cell).UpgradeCoreStatus();
            }
            if (cell.cellType == "RotSpeedCell") {
                ((RotSpeedCell) cell).CanCelEffects();
                ((RotSpeedCell) cell).delta *= 1.3f;
                ((RotSpeedCell) cell).UpgradeCoreStatus();
            }
        }
    }

    public override void GiveFeature(int id) {

    }

    public override void CancelFeatrue() {

    }

    public override void CancelFeatrue(int id) {

    }

}
