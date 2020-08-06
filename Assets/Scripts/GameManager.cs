using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 다른 스크립트에서 이 스크립트에 있는 함수를 호출할때 쓰임 (Singleton)

    public bool isPlayerAlive = true;
    public float scoreOnDead;
    public float timeOnDead;

    void Awake()
    {
        if (GameManager.instance == null)  // 게임시작했을때 이 instance가 없을때
            GameManager.instance = this;  // instance를 생성
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver(float score, float time) {
        isPlayerAlive = false;
        scoreOnDead = score;
        timeOnDead = time;
        // 게임오버 씬? 불러오기
        Invoke("LoadGameOverScene", 3f);
    }

    private void LoadGameOverScene() {
        SceneManager.LoadScene("GameOver");
    }

}
