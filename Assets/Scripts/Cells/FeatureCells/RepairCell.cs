using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인접 Cell들의 내구도를 지속적으로 회복해줌
public class RepairCell : FeatureCell
{   
    bool canHeal = true; // coolTime 제어용
    float coolTime = 0.1f;
    float repairAmount = 0.5f;

    void Update()
    {
        if (isAttached && canHeal) {
            canHeal = false;
            RepairAdjacentCells();
        }
    }

    void RepairAdjacentCells() {
        foreach(Cell cell in adjacentCells) {
            if (cell != null) {
                // 내구도가 닳아있다면 회복
                if (cell.durability < cell.maxDurability)
                    cell.durability += repairAmount;
                // 최대 체력 안 넘어가게 한다
                if (cell.durability > cell.maxDurability)
                    cell.durability = cell.maxDurability;
            }
        }
        StartCoroutine(WaitForCoolTime());
    }

    // 쿨타임 기다린다
    protected IEnumerator WaitForCoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        canHeal = true;
    }

    // 특정 인접 셀 하나에 Feature 부여 => FeatureCell에 다른 Cell이 붙었을 때
    // id : 0 ~ 5
    public override void GiveFeature(int id) {
        
    }

    // 특정 인접 셀 하나의 Feature 취소 => FeatureCell에 붙은 다른 Cell이 떨어질 때(파괴될 때)
    public override void CancelFeatrue(int id) {
        
    }
}
