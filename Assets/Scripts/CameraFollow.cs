using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라가 플레이어를 따라다니게 해주는 스크립트
public class CameraFollow : MonoBehaviour
{
    public float smoothTimeX, smoothTimeY;
    public Vector2 velocity;
	public GameObject player;
	public Vector2 minPos, maxPos;
	public bool bound;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    // FIXME: should use LateUpdate()
    void FixedUpdate()
    {
        // Mathf.SmoothDamp는 천천히 값을 증가시키는 메서드이다.
        // Vector3.SmoothDamp(Vector3 current, Vector3 target, ref Vector3 Velocity, float smoothTime, float maxSpeed, float deltaTime);
        float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        // 카메라 이동
		transform.position = new Vector3 (posX, posY, transform.position.z);

		if(bound) {
			//Mathf.Clamp(현재값, 최대값, 최소값);  현재값이 최대값까지만 반환해주고 최소값보다 작으면 그 최소값까지만 반환합니다.

			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minPos.x, maxPos.x),
                Mathf.Clamp (transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp (transform.position.z, transform.position.z, transform.position.z));
        }
    }
}
