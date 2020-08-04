using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격을 담당하는 Structure Cell의 기본형 (CoreCell과 데미지빼고는 같음)
public class GunCell : Cell
{
    public GameObject bullet; // Instantiate될 총알
    // public Transform FirePos; // 총알이 나갈 위치(정면)

    // coolTime 관리용
    private bool canAttack = true;

    // -----------------------------[FEATURE CHECK]-----------------------------
    [HideInInspector]
    public int haveGrow = 0;
    [HideInInspector]
    public int haveMagic = 0;
    // -----------------------------[GUNCELL STATUS]-----------------------------
    // [공격 관련 스탯]
    public float damage = 2.5f; // Core의 공격의 damage
    public float coolTime = 0.5f; // Core의 공격의 쿨타임
    public float range = 6; // 사정거리
    public float shotSpeed = 5; // 투사체 속도
    public float bulletSpread = 1; // 탄퍼짐 정도
    // --------------------------------------------------------------------------

    // Start is called before the first frame update
    protected override void Awake()
    {
        cellType = "GunCell";
        base.Awake();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 공격은 입력없이 지속적으로 발사됨
        if(isAttached && canAttack) {
            canAttack = false;
            FireAutomatically();
        }
    }

    // vitual => GunCell을 상속받는 Cell들이 오버라이드 가능
    // (마우스 커서 방향으로) 총알을 발사한다
    protected virtual void FireAutomatically()
    {
        Vector2 dir = GetNormalVector();
        // Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - transform.position;
        dir = SetBulletSpread(dir);
        MakeBullet(dir);
        StartCoroutine(WaitForCoolTime());
    }

    protected Vector2 SetBulletSpread(Vector2 dir) {
        Quaternion randomRot = Quaternion.Euler(new Vector3(0, 0, Random.Range(-5f * bulletSpread, 5f * bulletSpread)));
        return randomRot * dir;
    }

    // vitual => GunCell을 상속받는 Cell들이 오버라이드 가능
    // 플레이어의 스테이터스에 맞추어 총알을 만든다
    // 생성 과정에서 dir은 normalized 된다
    protected void MakeBullet(Vector2 dir) {
        GameObject tempObj = Instantiate(bullet, transform.position, transform.rotation);

        tempObj.layer = gameObject.layer; // 총알의 소유주 정하기

        Rigidbody2D tempRb = tempObj.GetComponent<Rigidbody2D>();
        tempRb.velocity = coreCell.rigidBody.velocity + dir.normalized * shotSpeed; // 총알 속도 설정

        Bullet tempBullet = tempObj.GetComponent<Bullet>();

        // Feature 활성화
        if (haveGrow != 0) {
            GrowBullet growBullet = tempBullet.GetComponent<GrowBullet>();
            growBullet.enabled = true;
            growBullet.growRate += (haveGrow - 1) * 0.5f;
        }
            
        if (haveMagic != 0) {
            tempBullet.GetComponent<MagicBullet>().enabled = true;
        }

        tempBullet.damage = this.damage; // 총알의 데미지 설정
        tempBullet.popTime = range / shotSpeed; // 총알의 사정거리 설정
    }

    // 쿨타임 기다린다
    protected IEnumerator WaitForCoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        canAttack = true;
    }

    protected float SinByDegree(float degree) {
        float radian = (float) (Mathf.PI * degree / 180.0);

        return Mathf.Sin(radian);
    }

    protected float CosByDegree(float degree) {
        float radian = (float) (Mathf.PI * degree / 180.0);

        return Mathf.Cos(radian);
    }

}
