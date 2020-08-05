using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeRangeScript : MonoBehaviour
{
    EnemyCell EnemyCoreCell; 
    void Start()
    {
        EnemyCoreCell = transform.parent.GetComponent<EnemyCell>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            EnemyCoreCell.playerNoticed = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {

    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            EnemyCoreCell.playerNoticed = false;
        }
    }    
}
