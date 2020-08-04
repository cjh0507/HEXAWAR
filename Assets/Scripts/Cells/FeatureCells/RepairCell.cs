using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접 Cell들의 내구도를 지속적으로 회복해줌
public class RepairCell : FeatureCell
{
    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        
    }

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
