using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Cell들이 공통적으로 가져야할 특성에 대해서 정의하는 부분
public class Cell : MonoBehaviour
{
    private List<Cell> adjacentCells; // Cell 혼자 있는 경우를 제외하면 1~6개의 원소를 가짐

    // Core cell로부터의 거리(Core의 coreDistance는 0이고 그 인접한 셀들의 coreDistance는 1인 식)
    // Cell을 부착할 때 초기화해주어야 한다.
    private int coreDistance; 

    // 현재 adjacentCells에 기반하여 coreDistance를 설정해주는 함수
    private void SetCoreDistance() {
        if (adjacentCells == null || adjacentCells.Count == 0) {
            // Cell 혼자인 경우
            this.coreDistance = 0;
            return;
        }
        // 맨 처음 CoreCell이 혼자일 때를 제외하면 모든 Cell은 adjacentCells에 적어도 하나의 원소가 있다.
        int minDist = adjacentCells[0].coreDistance;

        // 인접한 Cell들 중에서 coreDistance 최솟값 찾기
        foreach (Cell cell in adjacentCells) {
            int tempDist = cell.coreDistance; 
            if (minDist > tempDist) {
                minDist = tempDist;
            }
        }

        // 최솟값에 +1 한 것이 이 Cell의 coreDistance
        this.coreDistance = minDist + 1;
    }

    public int GetCoreDistance() {
        return coreDistance;
    }
}
