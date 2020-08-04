using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GunCel => Cooltime 감소
// BoosterCell => Delta 증가
public class FastCell : FeatureCell
{
    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        Cell targetCell = adjacentCells[id];
        if(targetCell != null && targetCell.cellType != "FeatureCell")
        {
            // CoreCell은 GunCell처럼 취급
            if (targetCell.cellType == "CoreCell") {
                ((CoreCell) targetCell).coolTime *= 0.77f; // 약 공속 30% 증가
            }

            if (targetCell.cellType == "GunCell") {
                ((GunCell) targetCell).coolTime *= 0.77f; // 약 공속 30% 증가
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
    
    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
