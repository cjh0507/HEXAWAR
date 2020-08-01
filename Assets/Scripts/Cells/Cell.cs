using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Cell들이 공통적으로 가져야할 특성에 대해서 정의하는 부분
public class Cell : MonoBehaviour
{
    public Cell[] adjacentCells = new Cell[6];
    public bool isAttached = false;
    public GameObject[] gluePoints = new GameObject[6];

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < gluePoints.Length; i++) 
        {
            gluePoints[i] = transform.GetChild(i+3).gameObject;
        }
    }

    void Update()
    {
        // if(isAttached) Debug.Log("fuck yea!");
    }

    public Vector2 getAbsPos() {
        return FindOrigin(gameObject, Vector2.zero);
    }

    // Core에 붙어 있지 않은 경우에 쓰면 안됨
    public Vector2 FindOrigin(GameObject obj, Vector2 curPos) {
        if (obj.tag == "Player") {
            Debug.Log("im parent");
            Debug.Log($"x : {obj.transform.position.x}, y : {obj.transform.position.y}");
            return curPos + (Vector2) obj.transform.position;
        }

        Debug.Log("finding parent");
        Debug.Log($"x : {obj.transform.localPosition.x}, y : {obj.transform.localPosition.y}");
        return FindOrigin(obj.transform.parent.gameObject, curPos + (Vector2) obj.transform.localPosition);
    }

    // // Core cell로부터의 거리(Core의 coreDistance는 0이고 그 인접한 셀들의 coreDistance는 1인 식)
    // // Cell을 부착할 때 초기화해주어야 한다.
    // private int coreDistance;

    // // 현재 adjacentCells에 기반하여 coreDistance를 설정해주는 함수
    // private void SetCoreDistance() {
        

    //     // if (adjacentCells == null || adjacentCells.Count == 0) {
    //     //     // Cell 혼자인 경우
    //     //     this.coreDistance = 0;
    //     //     return;
    //     // }
    //     // 맨 처음 CoreCell이 혼자일 때를 제외하면 모든 Cell은 adjacentCells에 적어도 하나의 원소가 있다.

    //     int minDist = adjacentCells[0].coreDistance;

    //     // 인접한 Cell들 중에서 coreDistance 최솟값 찾기
    //     foreach (Cell cell in adjacentCells) {
    //         int tempDist = cell.coreDistance; 
    //         if (minDist > tempDist) {
    //             minDist = tempDist;
    //         }
    //     }

    //     // 최솟값에 +1 한 것이 이 Cell의 coreDistance
    //     this.coreDistance = minDist + 1;
    // }

    // private bool isAlone() {

    //     foreach ( Cell cell in adjacentCells) {

    //     }

    //     return true;
    // }

    // public int GetCoreDistance() {
    //     return coreDistance;
    // }
}
