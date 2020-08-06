using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyCell의 기능을 정의하는 부분
// EnemyCell은 "체력", "이동", "시점"의 중심
// EnemyCell은 CoreCell과 같이 FeatureCell의 기능 또한 갖고 있으며(CoreCell에 붙은 Cell들은 강화됨), StructureCell의 기능도 함(이동 및 공격)
public class EnemyCell : CoreCell
{
    public bool playerNoticed = false;
    protected bool canMove = true;
    public float enemySpeedEff = 0.8f;
    public float stoppingDistance = 0.0f;

    GameObject player;
    Transform playerTr;
    Vector2 EnemyVec = new Vector2(0, 0);

    private float moveTimer = 0;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("PlayerCoreCell");
        playerTr = player.transform;
        StartCoroutine("Patrol");
    }
    protected override void FixedUpdate() {
        // 플레이어 발견 시에만 공격시작
        Rotate();
        if(playerNoticed) {
            if (canAttack) {
                canAttack = false;
                FireAutomatically();
            }
            StopCoroutine("Patrol");
            ChasePlayer();
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
    
    void ChasePlayer() {
        Vector3 curPlayerPos = playerTr.position;
        // 먼 상태 ()
        if (Vector2.Distance(transform.position, playerTr.position) > stoppingDistance)
            if (Mathf.Abs(rigidBody.velocity.magnitude) < speed * enemySpeedEff) {
                rigidBody.AddForce((curPlayerPos - transform.position) * acceleration);
            }
    }

    IEnumerator Patrol() {
        EnemyVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized;

        while ( moveTimer < 1.5) {
            if (Mathf.Abs(rigidBody.velocity.magnitude) < (speed / 5))
                rigidBody.AddForce(EnemyVec * acceleration);
            
            moveTimer += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        moveTimer = 0;
        StartCoroutine("Patrol");
    }

    protected override void Rotate() {
        // 플레이어가 인식범위 내에 들어왔을 경우
        if (playerNoticed) {
            // transform.Rotate(new Vector3(0, 0, Vector3.RotateTowards(transform.position, playerTr.position, 100f, 100.0f).z));
            // Vector2 playerNormal = playerTr.rotation * (Vector2.up);
            Vector2 myRot = transform.rotation * Vector2.up;
            //Quaternion toPlayerRot = Quaternion.Euler(playerTr.position - transform.position);
            
            Quaternion toPlayerRot = Quaternion.FromToRotation(myRot, playerTr.position - transform.position );
            //Debug.Log($"myRot: {myRot}, toPlayerRot : {toPlayerRot.eulerAngles}");
            if (toPlayerRot.eulerAngles.z > 185) 
                transform.Rotate(-toPlayerRot.eulerAngles.normalized * (rotSpeed * 200 / rigidBody.mass) * Time.deltaTime);
            else if (toPlayerRot.eulerAngles.z < 175) 
                transform.Rotate(toPlayerRot.eulerAngles.normalized * (rotSpeed * 200 / rigidBody.mass) * Time.deltaTime);
        }
        // 플레이어가 인식범위에 없을 경우 순찰모드
        else {
            
        }
    }

    
}
