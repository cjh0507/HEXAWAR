﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CoreCell의 기능을 정의하는 부분
// CoreCell은 Player의 "체력", "이동", "시점"의 중심
// CoreCell은 FeatureCell의 기능 또한 갖고 있으며(CoreCell에 붙은 Cell들은 강화됨), StructureCell의 기능도 함(이동 및 공격)
public class CoreCell : Cell
{
    private Rigidbody2D rigidBody;

    public GameObject bullet; // Instantiate될 총알
    public Transform FirePos; // 총알이 나갈 위치(정면)

    // coolTime 관리용
    private bool canAttack = true;

    // -----------------------------[PLAYER STATUS]-----------------------------
    [SerializeField]
    private float health; // Core의 체력
    // [공격 관련 스탯]
    [SerializeField]
    private float damage; // Core의 공격의 damage
    [SerializeField]
    private float coolTime; // Core의 공격의 쿨타임
    [SerializeField]
    public float range; // 사정거리
    [SerializeField]
    public float shotSpeed; // 투사체 속도

    // [이동 관련 스탯]
    [SerializeField]
    private float speed; // Core가 도달 가능한 "최대" 이동 속력
    [SerializeField]
    private float acceleration; // Core의 가속도 (얼마나 빠르게 최대 이동 속도에 도달하는가)
    [SerializeField]
    private float rotSpeed; // Core의 회전속도
    // -------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        FirePos = transform.Find("FrontPointer");
    }

    void FixedUpdate()
    {
        // 이동에 관한 입력에 반응해야 함
        SetVelocity();
        Rotate();
        // 공격은 입력없이 지속적으로 발사됨
        if(canAttack) {
            canAttack = false;
            FireAutomatically();
        }
    }

    // 물체의 속도를 지정한다
    private void SetVelocity()
    {
        Vector2 vector = GetVector(); // 플레이어 입력으로부터 벡터값을 받는다.

        if (Mathf.Abs(rigidBody.velocity.magnitude) < speed)
        {   
            // 최대 속력에 도달하지 못했으면 가속한다
            rigidBody.AddForce(vector * acceleration);
        } 
    }


    // 물체를 회전시킨다
    private void Rotate() {
        if (Input.GetKey(KeyCode.Q)) {
            transform.Rotate(new Vector3(0, 0, 1) * (rotSpeed*100) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.Rotate(new Vector3(0, 0, -1) * (rotSpeed*100) * Time.deltaTime);
        }
    }

    // 플레이어의 입력으로부터 벡터값을 얻는다.
    // CoreCell의 로컬 좌표계에서 벡터 계산
    private Vector2 GetVector() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        inputVector = transform.localRotation * inputVector; // 로컬 좌표계 기준의 벡터

        return inputVector.normalized;
    }

    // (마우스 커서 방향으로) 총알을 발사한다
    void FireAutomatically()
    {
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - transform.position;

        MakeBullet(dir, transform.rotation);
        StartCoroutine(WaitForCoolTime());
          
    }

    // 플레이어의 스테이터스에 맞추어 총알을 만든다
    void MakeBullet(Vector2 dir, Quaternion rotation) {
        GameObject tempObj = Instantiate(bullet, FirePos.position, rotation);

        Rigidbody2D tempRb = tempObj.GetComponent<Rigidbody2D>();
        tempRb.velocity = dir.normalized * shotSpeed; // 총알 속도 설정

        Bullet tempBullet = tempObj.GetComponent<Bullet>();
        tempBullet.damage = this.damage; // 총알의 데미지 설정
        tempBullet.popTime = range / shotSpeed; // 총알의 사정거리 설정
    }

    // 쿨타임 기다린다
    IEnumerator WaitForCoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        canAttack = true;
    }

}
