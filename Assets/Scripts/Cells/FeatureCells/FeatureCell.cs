using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접한 셀들에 어떠한 특성을 부여하는 셀
public abstract class FeatureCell : Cell
{
    void Start()
    {
        cellType = "FeatureCell";
    }

    // 인접 셀 전부에 Feature 부여 => FeatureCell이 혼자 있다가 어떤 tissue에 붙었을 때
    public abstract void GiveFeature();

    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public abstract void GiveFeature(int id);

    // 인접 셀들 전부의 Feature 취소 => FeatureCell이 떨어질 때(파괴될 때)
    public abstract void CancelFeatrue();

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public abstract void CancelFeatrue(int id);
}
