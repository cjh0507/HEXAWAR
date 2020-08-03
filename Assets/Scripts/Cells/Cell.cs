using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Cell들이 공통적으로 가져야할 특성에 대해서 정의하는 부분
public class Cell : MonoBehaviour
{
    // adjaCentCells 와 gluePoints의 index는 서로 대응된다(같은 index면 같은 위치)
    public Cell[] adjacentCells = new Cell[6];
    public GameObject[] gluePoints = new GameObject[6];

    public float durability = 20;

    public bool isAttached = false; // tissue에 소속되어 있는가?

    private Vector2[] localPosArr = {
        new Vector2(0, 0.866f), new Vector2(0.75f, 0.433f), new Vector2(0.75f, -0.433f), 
        new Vector2(0, -0.866f), new Vector2(-0.75f, -0.433f), new Vector2(-0.75f, 0.433f) 
    };

    private PolygonCollider2D polygonCollider2D;

    protected virtual void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        for (int i = 0; i < gluePoints.Length; i++) 
        {
            gluePoints[i] = transform.GetChild(i+2).gameObject;
        }
    }

    protected virtual void Update()
    {
        // if(isAttached) Debug.Log("fuck yea!");
    }

    // adjacentCell가 비어있는지 체크
    public bool isAlone() {
        foreach (Cell cell in adjacentCells) {
            if (cell != null)
                return false;
        }
        return true;
    }

    // this Cell과 oCell간의 부착이 일어날 때
    public void UpdateAdjacentCellsOnAttach() 
    {   
        // Raycast하기 전에 자기 자신 + GluePoints의 Collider를 꺼줘야 한다.
        polygonCollider2D.enabled = false;
        DisableGluePts();

        SetGluePtsAttachable(); // GluePoints가 모두 attachable하게 만든다
        // this Cell의 중심을 기준으로 6방향으로 RayCast를 해서 닿은 Cell이 있으면 서로의 adjacentCell에 추가한다.
        RaycastHit2D[] hits = new RaycastHit2D[6];

        for(int thisCellId = 0; thisCellId < hits.Length; thisCellId++) {
            hits[thisCellId] = Physics2D.Linecast(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId])) );
            // Debug.DrawLine(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId])), Color.white, 2f); // 나중에 지우자

            if (hits[thisCellId].collider != null && hits[thisCellId].collider.tag == "GluePoint") {
                Debug.Log($"{hits[thisCellId].collider.name} hit"); // 나중에 지우자
                GameObject hitGluePt = hits[thisCellId].collider.gameObject;
                Cell hitCell = hitGluePt.transform.parent.GetComponent<Cell>();

                adjacentCells[thisCellId] = hitCell;
                hitCell.adjacentCells[hitGluePt.GetComponent<GluePoint>().id] = this;
                
                gluePoints[(thisCellId) % 6 ].GetComponent<GluePoint>().isAttachable = false;
                gluePoints[(thisCellId+1) % 6 ].GetComponent<GluePoint>().isAttachable = false;
                gluePoints[(thisCellId+5) % 6 ].GetComponent<GluePoint>().isAttachable = false;
                // this.isAttached = true;
            }
        }
        isAttached = true;
        // Raycast 끝났으니 Collider 킨다
        EnableGluePts();
        polygonCollider2D.enabled = true;
    }

    public int SearchCell(Cell targetCell) {
        for (int i = 0; i < adjacentCells.Length; i++) {
            if(adjacentCells[i] == targetCell)
                return i;
        }
        return -1; // search failed
    }

    /*
    // adjacentCells에 따라 GluePoint 비활성화 
    public void UpdateAttachability() {
        if (this.gameObject.tag != "Player") {

        }
    }
    */

    void SetGluePtsAttachable() {
        foreach(GameObject obj in gluePoints) {
            obj.GetComponent<GluePoint>().isAttachable = true;
        }
    }

    public void DisableGluePts() {
        foreach (GameObject gluePt in gluePoints) {
            gluePt.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void EnableGluePts() {
        foreach (GameObject gluePt in gluePoints) {
            // if (gluePt.GetComponent<GluePoint>().isAttachable)
                gluePt.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    /*

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

    */

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
