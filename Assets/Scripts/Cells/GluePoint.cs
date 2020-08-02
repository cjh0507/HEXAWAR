using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluePoint : MonoBehaviour
{
    public GameObject coreCell; 
    public int id; // 0 ~ 5번 중 어느 위치의 gluePoint?
    public GameObject attachedCell; // 이 gluePoint가 부착된 Cell

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "GluePoint") {
            // 그 GluePoint  붙이기
            if (isAttachable) 
            {
                isAttachable = false;
                AttachCell(other);
                OnAttach(other.GetComponent<GluePoint>().attachedCell, other);
            }
        }
    }

    private void AttachCell(Collider2D other) {
        if (true)
        {
            GameObject otherCell = other.GetComponent<GluePoint>().attachedCell; // 부딪힌 GluePoint를 갖고 있는 Cell 찾기
            // otherCell을 이 GluePoint의 attachedCell의 자식으로 넣고, 이 GluePoint가 대표하는 위치로 놓기
            otherCell.transform.parent = attachedCell.transform;

            Cell aCell = attachedCell.GetComponent<Cell>();
            Cell oCell =  otherCell.GetComponent<Cell>();
            int oCellGPId = other.GetComponent<GluePoint>().id;
            
            // 부딪힌 attached Cell의 GluePoint에 따라서 localPos 정한다
            Vector2 localPos = localPosArr[this.id];
            // 충돌한 GluePoint 둘의 관계에 따라 localRotation을 설정한다
            Quaternion localRot = Quaternion.Euler(new Vector3(0, 0, 60 * (oCellGPId-this.id+3)));

            otherCell.transform.localPosition = localPos;
            otherCell.transform.localRotation = localRot;

            // 서로 부딪힌 Cell들의 adjacentCells 업데이트 - 아직 불완전함
            aCell.adjacentCells[this.id] = oCell;
            oCell.adjacentCells[oCellGPId] = aCell;

            oCell.isAttached = true;

            Debug.Log($"{oCell.name} Attached to {aCell.name} index {id} => pos: {otherCell.transform.localPosition} rot: {otherCell.transform.rotation}"); // 나중에 지워야 됨
        }
    }

    private void UpdateAdjacentCells(Cell cell) 
    {   

    }

    private void OnAttach(GameObject other_AttachedCell, Collider2D other) {

        Cell oCell = other_AttachedCell.GetComponent<Cell>();
        int otherGluePtID = other.GetComponent<GluePoint>().id;
        GameObject[] oGluePoints = oCell.gluePoints;

        oGluePoints[(otherGluePtID+2)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+3)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+4)%6].GetComponent<GluePoint>().isAttachable = true;

        Debug.Log($"{otherGluePtID} collided"); // 나중에 지워야 됨
    }
}
