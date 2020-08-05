using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyCell의 기능을 정의하는 부분
// EnemyCell은 "체력", "이동", "시점"의 중심
// EnemyCell은 CoreCell과 같이 FeatureCell의 기능 또한 갖고 있으며(CoreCell에 붙은 Cell들은 강화됨), StructureCell의 기능도 함(이동 및 공격)
public class EnemyCell : CoreCell
{
    public bool playerNoticed = false;
    private bool canMove = true;

    GameObject player;
    Transform playerTr;
    Vector2 EnemyVec = new Vector2(0, 0);

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("PlayerCoreCell");
        playerTr = player.transform;
        StartCoroutine("Patrol");
    }
    protected override void FixedUpdate() {
        // 플레이어 발견 시에만 공격시작
        if(playerNoticed && canAttack) {
            canAttack = false;
            FireAutomatically();
        }
    }

    protected override void SetVelocity() {
        // 플레이어가 인식범위 내에 들어왔을 경우
        // if (playerNoticed) {
        //     Vector2 curTr = player.transform.position;
        // }
        // 플레이어가 인식범위에 없을 경우 순찰모드
        // else {
        //     Vector2 vector = GetVector();
        //     if (Mathf.Abs(rigidBody.velocity.magnitude) < speed)
        //     {
        //         rigidBody.AddForce(vector * acceleration);
        //     }
        // }
    }

    IEnumerator Patrol() {
        EnemyVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        if (Mathf.Abs(rigidBody.velocity.magnitude) < speed)
            rigidBody.AddForce(EnemyVec * acceleration * 10);
        
        yield return new WaitForSeconds(4f);
        StartCoroutine("Patrol");
    }

    protected override void Rotate() {
        // 플레이어가 인식범위 내에 들어왔을 경우
        if (playerNoticed) {
            
        }
        // 플레이어가 인식범위에 없을 경우 순찰모드
        else {
            
        }

    }

    
}
