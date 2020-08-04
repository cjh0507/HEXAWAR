using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Cell들이 공통적으로 가져야할 특성에 대해서 정의하는 부분
public class Cell : MonoBehaviour
{
    // -----------------------------[NETWORK VARS]------------------------------
    public Transform tr;
    public PhotonView pv;

    public Vector3 currPos;
    public Quaternion currRot;
    

    // adjaCentCells 와 gluePoints의 index는 서로 대응된다(같은 index면 같은 위치)
    public Cell[] adjacentCells = new Cell[6];
    public GameObject[] gluePoints = new GameObject[6];

    [HideInInspector]
    public Rigidbody2D rigidBody;

    // [SerializeField]
    protected CoreCell coreCell;

    public string cellType;

    public float maxDurability = 50;
    public float durability = 50;

    public float mass = 0.20f;

    public bool isAttached = false; // tissue에 소속되어 있는가?

    private Vector2[] localPosArr = {
        new Vector2(0, 0.866f), new Vector2(0.75f, 0.433f), new Vector2(0.75f, -0.433f), 
        new Vector2(0, -0.866f), new Vector2(-0.75f, -0.433f), new Vector2(-0.75f, 0.433f) 
    };

    private PolygonCollider2D polygonCollider2D;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = this.mass;
        durability = maxDurability;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        for (int i = 0; i < gluePoints.Length; i++) 
        {
            gluePoints[i] = transform.GetChild(i+1).gameObject;
        }
    }

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        pv.ObservedComponents[0] = this;
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
    // 1. adjacentCells 업데이트 하기
    // 2. FeatureCells 체크
    public void OnAttach() 
    {   
        // rigidBody 제거
        Destroy(rigidBody);
        // Raycast하기 전에 자기 자신 + GluePoints의 Collider를 꺼줘야 한다.
        polygonCollider2D.enabled = false;
        DisableGluePts();

        // 코어의 질량 늘리기
        coreCell.IncreaseMass(this.mass);

        SetGluePtsAttachable(); // GluePoints가 모두 attachable하게 만든다
        // this Cell의 중심을 기준으로 6방향으로 RayCast를 해서 닿은 Cell이 있으면 서로의 adjacentCell에 추가한다.
        RaycastHit2D[] hits = new RaycastHit2D[6];

        for(int thisCellId = 0; thisCellId < hits.Length; thisCellId++) {
            hits[thisCellId] = Physics2D.Linecast(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId])) );
            // Debug.DrawLine(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId])), Color.white, 2f); // 나중에 지우자

            if (hits[thisCellId].collider != null && hits[thisCellId].collider.tag == "GluePoint") {
                Debug.Log($"{hits[thisCellId].collider.name} hit"); // 나중에 지우자
                GameObject hitGluePt = hits[thisCellId].collider.gameObject;
                int hitGluePtId = hitGluePt.GetComponent<GluePoint>().id;
                Cell hitCell = hitGluePt.transform.parent.GetComponent<Cell>();

                // this Cell의 adjacentCells에 hitCell 추가
                adjacentCells[thisCellId] = hitCell;
                // hitCell의 adjacentCells에 this Cell 추가
                hitCell.adjacentCells[hitGluePtId] = this;
                
                gluePoints[(thisCellId) % 6 ].GetComponent<GluePoint>().isAttachable = false;
                gluePoints[(thisCellId+1) % 6 ].GetComponent<GluePoint>().isAttachable = false;
                gluePoints[(thisCellId+5) % 6 ].GetComponent<GluePoint>().isAttachable = false;

                // hitCell이 FeatureCell이었다면
                if(hitCell.cellType == "FeatureCell") {
                    // this Cell에게 Feature 적용
                    ((FeatureCell) hitCell).GiveFeature(hitGluePtId);
                }
            }
        }
        
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

    protected Vector2 GetNormalVector() {
        return transform.rotation * Vector2.up;
    }

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

    public void FindCore() {
        if(isAttached)
            coreCell = transform.parent.gameObject.GetComponent<CoreCell>();
    }

    public CoreCell GetCoreCell() {
        return coreCell;
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
