using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Cell들이 공통적으로 가져야할 특성에 대해서 정의하는 부분
public class Cell : MonoBehaviour
{
    // adjaCentCells 와 gluePoints의 index는 서로 대응된다(같은 index면 같은 위치)
    public Cell[] adjacentCells = new Cell[6];
    public GameObject[] gluePoints = new GameObject[6];

    [HideInInspector]
    public Rigidbody2D rigidBody;

    [SerializeField]
    protected CoreCell coreCell;

    public string cellType;

    private CameraFollow cameraFollow;


    // -----------------------------------[CELL STATUS]-----------------------------------
    public float maxDurability = 50;
    public float durability = 50;

    public float mass = 0.20f;
    // -----------------------------------------------------------------------------------
    public bool isAttached = false; // tissue에 소속되어 있는가?

    private Vector2[] localPosArr = {
        new Vector2(0, 0.866f), new Vector2(0.75f, 0.433f), new Vector2(0.75f, -0.433f), 
        new Vector2(0, -0.866f), new Vector2(-0.75f, -0.433f), new Vector2(-0.75f, 0.433f) 
    };

    [SerializeField]
    public PolygonCollider2D polygonCollider2D;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null) {
            rigidBody.mass = this.mass;
        }
        durability = maxDurability;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        for (int i = 0; i < gluePoints.Length; i++) 
        {
            gluePoints[i] = transform.GetChild(i+1).gameObject;
        }
        FindCore();
        cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    protected virtual void Update() {

    }
    
    protected bool isDead() { return durability < 0; }


    protected virtual void Die() {

        // <TODO> : 적절한 애니메이션(이펙트) 실행
        Decompose();
    }

    protected virtual void Decompose() {
        if(cellType == "CoreCell") {
            SetGluePtsAttachable(false);
            foreach(Transform child in transform) {
                if(Bullet.isCell(child.tag))
                    child.GetComponent<Cell>().SetGluePtsAttachable(false);
                // Debug.Log(child.name);
            }

            List<Cell> cells = new List<Cell>();

            foreach(Transform child in transform) {
                // 자식 셀들에게 할 일
                // 1. layer를 Default로 바꾸고 (done)
                // 2. rigidBody를 활성화시킨다 (GravityScale = 0, linearDrag = 0.5, AngularDrag = 1) (done)
                // 3. isAttached = false, coreCell = null, 스탯 초기화, adjacentCells 초기화
                // 4. gluePoints 활성화 및 gluePoints의 coreCell = null (done)
                // 5. 부모로부터 독립시킨다 (done)

                // Debug.Log(child.name);
                if(Bullet.isCell(child.tag)) {
                    // [Step 1]
                    ChangeLayersRecursively(child, 0);

                    // [Step 2]
                    Rigidbody2D childRb = child.gameObject.AddComponent<Rigidbody2D>();
                    childRb.gravityScale = 0;
                    childRb.drag = 0.5f;
                    childRb.angularDrag = 1;
                    childRb.velocity = rigidBody.velocity + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)); // TODO : 코어로부터 멀어지는 방향으로 고치기

                    Cell childCell = child.GetComponent<Cell>();
                    childCell.rigidBody = childRb;
                    
                    // [Step 3]
                    childCell.isAttached = false;
                    childCell.coreCell = null;
                    childCell.ResetStatus();
                    // Here : adjacentCells 초기화
                    childCell.adjacentCells = new Cell[6];
                    
                    // [Step 4]
                    childCell.EnableGluePts();
                    childCell.SetGluePtsAttachable(false);
                    childCell.SetGluePtsCoreCellNull();

                    // [Step 5]
                    cells.Add(childCell);
                } 
            }
                foreach(Cell cell in cells) {
                    cell.transform.parent = null;
            }
        } else {
            CoreCell tempCoreCell = coreCell;
            // 일반 셀이 부서졌을 때
            SetGluePtsAttachable(false);
            // Debug.Log("cell dead");
             
            foreach(Transform child in coreCell.transform) {
                if(Bullet.isCell(child.tag)) {
                    child.GetComponent<Cell>().SetGluePtsAttachable(false);
                }
            }
            coreCell.DisableGluePts();
            List<Cell> cells = new List<Cell>();
            
            foreach(Transform child in coreCell.transform) {
                // 자식 셀들에게 할 일
                // 1. layer를 Default로 바꾸고 (done)
                // 2. rigidBody를 활성화시킨다 (GravityScale = 0, linearDrag = 0.5, AngularDrag = 1) (done)
                // 3. isAttached = false, coreCell = null, 스탯 초기화, adjacentCells 초기화
                // 4. gluePoints 활성화 및 gluePoints의 coreCell = null (done)
                // 5. 부모로부터 독립시킨다 (done)
                
                // Debug.Log(child.name);
                if(Bullet.isCell(child.tag)) {
                    // [Step 1]
                    ChangeLayersRecursively(child, 0);
                    // Debug.Log($"{child.name} step 1 reached");
                    // Debug.Log($" child of {coreCell.name} -> {child.name} step 1 reached");
                    // [Step 2]
                    Rigidbody2D childRb = child.gameObject.AddComponent<Rigidbody2D>();
                    childRb.gravityScale = 0;
                    childRb.drag = 0.5f;
                    // Debug.Log($" child of {coreCell.name} -> {child.name} step 2 reached");
                    childRb.velocity = coreCell.rigidBody.velocity;
                    childRb.angularDrag = 1;
                    
                    Cell childCell = child.GetComponent<Cell>();
                    childCell.rigidBody = childRb;
                    
                    // [Step 3]
                    childCell.isAttached = false;
                    childCell.ResetStatus();
                    // Here : adjacentCells 초기화
                    childCell.adjacentCells = new Cell[6];
                    
                    // [Step 4]
                    childCell.EnableGluePts();
                    childCell.SetGluePtsAttachable(false);
                    childCell.SetGluePtsCoreCellNull();
                    
                    // [Step 5]
                    cells.Add(childCell);
                } 
            }
            Destroy(gameObject);
            foreach(Cell cell in cells) {
                cell.coreCell = null;
                cell.transform.parent = null;
            }
            tempCoreCell.ResetStatus();
            tempCoreCell.adjacentCells = new Cell[6];
            tempCoreCell.EnableGluePts();
            tempCoreCell.SetGluePtsAttachable(true);
        }
        Destroy(gameObject);
    }

    // mass, (damage, coolTime, range, shotSpeed, haveGrow, haveMagic), (delta)
    protected virtual void ResetStatus() {
        mass = 0.2f;
        maxDurability = 50;
        if (durability > maxDurability)
            durability = maxDurability;
    }

    // Cell이 적대적인 총알에 맞았을 때 호출되는 함수
    public void CellHit(float damage) {

        // <TODO> : 적절한 애니메이션(이펙트) 실행

        durability -= damage;

        // 죽었는지 체크
        if (isDead()) {
            Die();
        }
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
    public IEnumerator OnAttach() 
    {   
        // rigidBody 제거
        Destroy(rigidBody);
        // Raycast하기 전에 자기 자신 + GluePoints의 Collider를 꺼줘야 한다.
        DisableGluePts();
        polygonCollider2D.enabled = false;

        // 코어의 질량 늘리기
        coreCell.IncreaseMass(this.mass);

        SetGluePtsAttachable(true); // GluePoints가 모두 attachable하게 만든다
        // this Cell의 중심을 기준으로 6방향으로 RayCast를 해서 닿은 Cell이 있으면 서로의 adjacentCell에 추가한다.
        RaycastHit2D[] hits = new RaycastHit2D[6];

        for(int thisCellId = 0; thisCellId < hits.Length; thisCellId++) {
            hits[thisCellId] = Physics2D.Linecast(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId])) * 1.1f );
            Debug.DrawLine(transform.position, transform.position + (transform.rotation * (localPosArr[thisCellId]) * 1.1f), Color.white, 5f); // 나중에 지우자

            if (hits[thisCellId].collider != null && hits[thisCellId].collider.tag == "GluePoint") {
                // Debug.Log($"LineCast from {gameObject.name} hit {hits[thisCellId].collider.name}"); // 나중에 지우자
                GameObject hitGluePt = hits[thisCellId].collider.gameObject;

                int hitGluePtId = hitGluePt.GetComponent<GluePoint>().id;
                Cell hitCell = hitGluePt.transform.parent.GetComponent<Cell>();

                if(hitCell.isAttached)
                {
                    // this Cell의 adjacentCells에 hitCell 추가
                    adjacentCells[thisCellId] = hitCell;
                    // hitCell의 adjacentCells에 this Cell 추가
                    hitCell.adjacentCells[hitGluePtId] = this;
                    
                    gluePoints[(thisCellId) % 6].GetComponent<GluePoint>().isAttachable = false;
                    gluePoints[(thisCellId+1) % 6].GetComponent<GluePoint>().isAttachable = false;
                    gluePoints[(thisCellId+5) % 6].GetComponent<GluePoint>().isAttachable = false;

                    // hitCell이 FeatureCell이었다면
                    if(hitCell.cellType == "FeatureCell") {
                        // this Cell에게 Feature 적용
                        ((FeatureCell) hitCell).GiveFeature(hitGluePtId);
                    }
                }
            }
        }
        
        // Raycast 끝났으니 Collider 킨다
        polygonCollider2D.enabled = true;
        EnableGluePts();
        
        if (gameObject.layer == LayerMask.NameToLayer("Player")) {
            Debug.Log($"reached");
            cameraFollow.ChangeSize(coreCell.CountChildren());
        }
        yield return null;
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

    void SetGluePtsAttachable(bool boolV) {
        foreach(GameObject obj in gluePoints) {
            obj.GetComponent<GluePoint>().isAttachable = boolV;
        }
    }

    void SetGluePtsCoreCellNull() {
        foreach(GameObject obj in gluePoints) {
            obj.GetComponent<GluePoint>().coreCell = null;
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
        if (cellType == "CoreCell")
            return;
        if (isAttached)
            coreCell = transform.parent.gameObject.GetComponent<CoreCell>();
    }

    public CoreCell GetCoreCell() {
        return coreCell;
    }

    public void ChangeLayer(int layerNum) {
        ChangeLayersRecursively(transform, layerNum);
    }
    
    public void ChangeLayersRecursively(Transform trans, int layerNum){
        trans.gameObject.layer = layerNum;
        foreach(Transform child in trans)
        {
            ChangeLayersRecursively(child, layerNum);
        }
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
