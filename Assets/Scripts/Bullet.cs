using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 총알의 충돌 이벤트와 제거 관리하는 스크립트
public class Bullet : MonoBehaviour
{
    public float popTime;
    public float damage;

    public GameObject popEffect;

    const int playerLayer = 8;
    const int enemyLayer = 9;

    // Start is called before the first frame update
    void Start()
    {
        ScaleByDamage();
        Invoke("DestroyBullet", popTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // 총알이 어딘가에 닿았을 때 호출되는 함수
    void OnTriggerEnter2D(Collider2D other)
    {   
        int otherLayer = other.gameObject.layer;

        // 충돌한 물체들의 소유주가 다를 때(대부분, 적대적일 때)
        if ((other.gameObject.layer != gameObject.layer)) {
            // 총알끼리는 충돌하지 않음
            if(other.tag == "Bullet" || other.tag == "GluePoint")
                return;

            if(isCell(other)) {
                if (otherLayer != playerLayer && otherLayer != enemyLayer)
                    return;
                other.GetComponent<Cell>().CellHit(damage);
                ShowPopEffect();
                DestroyBullet();
            }
        }
    }

    void ShowPopEffect() {
        GameObject tempEff = Instantiate(popEffect, transform.position, Quaternion.identity);
        foreach (Transform child in tempEff.transform) {
            child.localScale *= transform.localScale.x * 2;
        }
    }

    public static bool isCell(Collider2D other) {
        string tag = other.tag;
        return (tag == "Cell") || (tag == "GunCell") || (tag == "BoosterCell") || (tag == "FeatureCell") || (tag == "PlayerCoreCell");
    }

    public static bool isCell(string tag) {
        return (tag == "Cell") || (tag == "GunCell") || (tag == "BoosterCell") || (tag == "FeatureCell") || (tag == "PlayerCoreCell");
    }

    public void ScaleByDamage() {
        float scale = (damage - 5) * 0.1f;
        transform.localScale = new Vector3((1 + scale) * 2f, 1 + scale) * 0.1f;
    }
}
