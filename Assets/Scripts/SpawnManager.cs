using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] minions;
    public GameObject[] soldiers;
    public GameObject[] bosses;

    public GameObject[] cells;

    public Camera cam;

    float minionTime;
    float soldierTime;
    float bossTime;
    float cellTime;

    bool boss1Spawned = false;
    bool boss2Spawned = false;
    bool boss3Spawned = false;
    bool boss4Spawned = false;

    private Transform playerTr; // 플레이어 위치 추적용

    void Start() {
        playerTr = GameObject.FindWithTag("PlayerCoreCell").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        minionTime = 0;
        soldierTime = 20;
        bossTime = 0;
        cellTime = 0;
        SpawnMinion();
        SpawnCell();
        SpawnCell();
    }

    private void FixedUpdate() {
        if(!boss1Spawned && Input.GetKey(KeyCode.Alpha7)) {
            boss1Spawned = true;
            SpawnBoss(0);
        }
        if(!boss2Spawned && Input.GetKey(KeyCode.Alpha8)) {
            boss2Spawned = true;
            SpawnBoss(1);
        }
        if(!boss3Spawned && Input.GetKey(KeyCode.Alpha9)) {
            boss3Spawned = true;
            SpawnBoss(2);
        }
        if(!boss4Spawned && Input.GetKey(KeyCode.Alpha0)) {
            boss4Spawned = true;
            SpawnBoss(3);
        }
        if (InGameUI.instance.time - minionTime > 10) {
            minionTime += 10;
            SpawnMinion();
        }

        if (InGameUI.instance.time - soldierTime > 40) {
            soldierTime += 40;
            SpawnSoldier();
        }

        if (InGameUI.instance.time - bossTime > 180) {
            bossTime += 180;
            SpawnBoss();
        }

        if (InGameUI.instance.time - cellTime > 20) {
            cellTime += 20;
            SpawnCell();
        }
    }

    Vector2 GetRandomPos(float distCoef) {

        // 화면 사이즈의 반에 해당하는 벡터를 초기화 해준다.
        Vector2 originPos = new Vector2(cam.orthographicSize * 16 / 9, cam.orthographicSize) * distCoef;
        // 랜덤한 각도를 지정한다.
        float randomAngle = Random.Range(0, 360);
    
        // 랜덤한 각도에 해당하는 x, y좌표를 삼각함수를 이용해 초기화해준다.
        float randomX = Mathf.Cos(randomAngle) * originPos.x;
        float randomY = Mathf.Sin(randomAngle) * originPos.y;

        return new Vector2(randomX, randomY);
    }

    void SpawnMinion() {
        int randId = Random.Range(0, minions.Length);
        
        Vector2 randPos = (Vector2) playerTr.position + GetRandomPos(1.2f);
        Instantiate(minions[randId], randPos, Quaternion.identity);
    }

    void SpawnSoldier() {
        int randId = Random.Range(0, soldiers.Length);
        
        Vector2 randPos = (Vector2) playerTr.position + GetRandomPos(1.2f);
        Instantiate(soldiers[randId], randPos, Quaternion.identity);
    }

    void SpawnBoss() {
        int randId = Random.Range(0, bosses.Length);
        
        Vector2 randPos = (Vector2) playerTr.position + GetRandomPos(1.5f);
        Instantiate(bosses[randId], randPos, Quaternion.identity);
    }

    void SpawnBoss(int id) {
        Vector2 randPos = (Vector2) playerTr.position + GetRandomPos(1.0f);
        Instantiate(bosses[id], randPos, Quaternion.identity);
    }

    void SpawnCell() {
        int randId = Random.Range(0, cells.Length);
        
        Vector2 randPos = (Vector2) playerTr.position + GetRandomPos(0.5f);
        Instantiate(cells[randId], randPos, Quaternion.identity);
    }
}
