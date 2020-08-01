using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluePoint : MonoBehaviour
{
    public GameObject coreCell; 
    public int id; // 0 ~ 5번 중 어느 위치의 gluePoint?
    public GameObject attachedCell; // 이 gluePoint가 부착된 Cell

    public bool isAttachable = false;
    
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
                AttachCell(other);
                OnAttach(other.GetComponent<GluePoint>().attachedCell, other);
                isAttachable = false;
            }
        }
    }

    private void AttachCell(Collider2D other) {
        if (true)
        {
            GameObject otherCell = other.GetComponent<GluePoint>().attachedCell; // 부딪힌 GluePoint를 갖고 있는 Cell 찾기
            // otherCell을 이 GluePoint의 attachedCell의 자식으로 넣고, 이 GluePoint가 대표하는 위치로 놓기
            otherCell.transform.parent = attachedCell.transform; // 이거 맞아??? 
            otherCell.transform.rotation = Quaternion.identity;
            Vector2 localPos = Vector2.zero;
            Quaternion localRot = Quaternion.identity;

            Cell aCell = attachedCell.GetComponent<Cell>();
            Cell oCell =  otherCell.GetComponent<Cell>();
            int oCellGPId = other.GetComponent<GluePoint>().id;
            
            switch (this.id) {
                case 0:
                    localPos.x = 0;
                    localPos.y = 0.866f;
                    aCell.adjacentCells[0] = oCell;
                    //localRot.z = 0;
                    break;
                case 1:
                    localPos.x = 0.75f;
                    localPos.y = 0.433f;
                    aCell.adjacentCells[1] = oCell;
                    //localRot.z = ;
                    break;
                case 2:
                    localPos.x = 0.75f;
                    localPos.y = -0.433f;
                    aCell.adjacentCells[2] = oCell;
                    break;
                case 3:
                    localPos.x = 0;
                    localPos.y = -0.866f;
                    aCell.adjacentCells[3] = oCell;
                    break;
                case 4:
                    localPos.x = -0.75f;
                    localPos.y = -0.433f;
                    aCell.adjacentCells[4] = oCell;
                    break;
                case 5:
                    localPos.x = -0.75f;
                    localPos.y = 0.433f;
                    aCell.adjacentCells[5] = oCell;
                    break;
            }
            oCell.adjacentCells[oCellGPId] = aCell;
            oCell.isAttached = true;

            localPos = (attachedCell.transform.localRotation) * localPos; 
            otherCell.transform.position = aCell.getAbsPos() + localPos;
            // otherCell.transform.position = (Vector2) attachedCell.transform.position + localPos; // ***************


            otherCell.transform.rotation = coreCell.GetComponent<Transform>().rotation; // ******************
            Debug.Log($"{oCell.name} Attached to {aCell.name} index {id} => pos: {otherCell.transform.position} rot: {otherCell.transform.rotation}"); // 나중에 지워야 됨
        }
    }


    private void OnAttach(GameObject other_AttachedCell, Collider2D other) {
        Cell oCell = other_AttachedCell.GetComponent<Cell>();
        int otherGluePtID = other.GetComponent<GluePoint>().id;
        GameObject[] oGluePoints = oCell.gluePoints;
        oGluePoints[(otherGluePtID+2)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+3)%6].GetComponent<GluePoint>().isAttachable = true;
        oGluePoints[(otherGluePtID+4)%6].GetComponent<GluePoint>().isAttachable = true;
        //foreach (GameObject gluePt in oGluePoinets) {
        //    gluePt.GetComponent<GluePoint>().isAttachable = true;
        //}
    }
}
