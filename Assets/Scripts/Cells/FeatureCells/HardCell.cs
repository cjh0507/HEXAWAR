using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접 Cell들의 최대 내구도 증가(아이작 같은 식으로 증가한 내구도 만큼 회복)
public class HardCell : FeatureCell
{
    float delta;
    protected override void Start() {
        durability = maxDurability;
        base.Start();
    }

    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        Cell targetCell = adjacentCells[id];
        if(targetCell != null) {
            // CoreCell은 조금 덜 늘어나게
            if (targetCell.cellType == "CoreCell") {
                delta = targetCell.maxDurability * 0.2f;
            }
            else {
                delta = targetCell.maxDurability * 0.3f;
            }
            
            // 최대체력이 늘어난 만큼 회복
            targetCell.maxDurability += delta;
            targetCell.durability += delta;
            if (targetCell.durability > targetCell.maxDurability)
                targetCell.durability = targetCell.maxDurability;
        }
    }

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
