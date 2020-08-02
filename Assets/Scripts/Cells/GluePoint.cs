using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluePoint : MonoBehaviour
{
    public int id; // 0 ~ 5번 중 어느 위치의 gluePoint?
    public GameObject attachedCell; // 이 gluePoint가 부착된 Cell
    private GameObject coreCell; // 중심 셀

    public bool isAttachable = false;

    private Vector2[] localPosArr = {
        new Vector2(0, 0.866f), new Vector2(0.75f, 0.433f), new Vector2(0.75f, -0.433f), 
        new Vector2(0, -0.866f), new Vector2(-0.75f, -0.433f), new Vector2(-0.75f, 0.433f) 
    };
    
    // Start is called before the first frame update
    void Start()
    {
        attachedCell = transform.parent.gameObject;
        coreCell = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "GluePoint") {
            // 그 GluePoint  붙이기
            if (isAttachable) 
            {
                isAttachable = false;

                // 부딪히면 잠시 동안 두 Cell의 GluePoint들의 Collider를 모두 비활성화 시킨다
                // attachedCell.GetComponent<Cell>().DisableGluePts();
                // other.GetComponent<GluePoint>().attachedCell.GetComponent<Cell>().DisableGluePts();
                // StartCoroutine("DelayedEnableGluePts", other);

                AttachCell(other);
                // OnAttach(other.GetComponent<GluePoint>().attachedCell, other);
            }

        }
    }

    private void AttachCell(Collider2D other) {
        if (true)
        {
            GameObject otherCell = other.GetComponent<GluePoint>().attachedCell; // 부딪힌 GluePoint를 갖고 있는 Cell 찾기
            // otherCell을 CoreCell의 자식으로 넣고, 이 GluePoint가 대표하는 위치로 놓기
            otherCell.transform.parent = coreCell.transform;

            Cell aCell = attachedCell.GetComponent<Cell>();
            Cell oCell =  otherCell.GetComponent<Cell>();
            int oCellGPId = other.GetComponent<GluePoint>().id;
            
            // 부딪힌 attached Cell의 GluePoint에 따라서 localPos 정한다
            Vector2 localPos = localPosArr[this.id];
            // 충돌한 GluePoint 둘의 관계에 따라 localRotation을 설정한다
            Quaternion localRot = Quaternion.Euler(new Vector3(0, 0, 60 * (oCellGPId-this.id+3)));

            if(attachedCell == coreCell) {
                otherCell.transform.localPosition = localPos;
            }
                
            else 
            {
                otherCell.transform.localPosition = (Vector2) attachedCell.transform.localPosition + (Vector2) (attachedCell.transform.localRotation * localPos);
                localRot = attachedCell.transform.localRotation * localRot;
            }
            otherCell.transform.localRotation = localRot;

            // 서로 부딪힌 Cell들의 adjacentCells 업데이트 - 아직 불완전함
            oCell.UpdateAdjacentCells();
            // aCell.UpdateAdjacentCells(oCell, this.id);
            // aCell.UpdateAttachability();
            
            // oCell.adjacentCells[oCellGPId] = aCell;

            oCell.isAttached = true;

            Debug.Log($"{oCell.name} index {oCellGPId} Attached to {aCell.name} index {id}"); // 나중에 지워야 됨
        }
    }

    private void OnAttach(GameObject other_AttachedCell, Collider2D other) {

        Cell oCell = other_AttachedCell.GetComponent<Cell>();
        int otherGluePtID = other.GetComponent<GluePoint>().id;
        GameObject[] oGluePoints = oCell.gluePoints;

        // 결합한 셀의 adjacentCells를 고려하지 않은 경우 
        oGluePoints[(otherGluePtID+2)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+3)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+4)%6].GetComponent<GluePoint>().isAttachable = true;

        Debug.Log($"otherGluePtId : {otherGluePtID} collided"); // 나중에 지워야 됨
    }

    IEnumerator DelayedEnableGluePts(Collider2D other) {
        yield return new WaitForSeconds(0.4f);
        attachedCell.GetComponent<Cell>().EnableGluePts();
        // gameObject.GetComponent<BoxCollider2D>().enabled = false;
        other.GetComponent<GluePoint>().attachedCell.GetComponent<Cell>().EnableGluePts();
        other.GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }
}
