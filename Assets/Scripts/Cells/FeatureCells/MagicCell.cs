using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GunCell => 유도기능 추가
public class MagicCell : FeatureCell
{
    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        Cell targetCell = adjacentCells[id];
        if(targetCell != null && targetCell.cellType != "FeatureCell")
        {
            // CoreCell은 GunCell처럼 취급
            if (targetCell.cellType == "CoreCell") {
                ((CoreCell) targetCell).haveMagic++;
            }

            if (targetCell.cellType == "GunCell") {
                ((GunCell) targetCell).haveMagic++;
            }

        }
    }
    
    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
