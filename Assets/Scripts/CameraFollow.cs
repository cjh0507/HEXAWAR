using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라가 플레이어를 따라다니게 해주는 스크립트
public class CameraFollow : MonoBehaviour
{
    public float smoothTimex, smoothTimeY;
    public Vector2 velocity;
    public GameObject player;
    public Vector2 minPos, maxPos;
    public bool bound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void LastUpdate()
    {
        // Vector3.SmoothDamp(Vector3 current, Vector3 target, ref Vector3 Velocity, float smoothTime, float maxSpeed, float deltaTime);
        // float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);

    }
}
