using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접 셀 질량을 가볍게 함 (절반?)
public class LiteCell : FeatureCell
{
    
    protected override void Start() {
        // mass = 0.1f;
        base.Start();
    }

    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        Cell targetCell = adjacentCells[id];
        if(targetCell != null) {
            coreCell.DecreaseMass(targetCell.mass);

            if (targetCell.cellType == "CoreCell")
                targetCell.mass *= 0.7f;
            else
                targetCell.mass *= 0.5f;
            
            coreCell.IncreaseMass(targetCell.mass);
        }
    }

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {

    }
}
