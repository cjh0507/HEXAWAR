using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접 Cell들의 최대 내구도 증가(아이작 같은 식으로 증가한 내구도 만큼 회복)
public class HardCell : FeatureCell
{
    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        
    }

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
