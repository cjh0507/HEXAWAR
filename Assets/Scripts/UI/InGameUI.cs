using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject inGameUI;
    public static InGameUI instance;
    public Text timeText;
    public Text scoreText;
    public Image panel;
    private CoreCell player;
    public float time;
    public float score;

    // Start is called before the first frame update
    void Start()
    {  
        score = 0;
        time = 0;
        player = GameObject.FindWithTag("PlayerCoreCell").GetComponent<CoreCell>();
        if (InGameUI.instance == null)  // 게임시작했을때 이 instance가 없을때
            InGameUI.instance = this;  // instance를 생성
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPlayerAlive) {
            if(PauseMenu.GetPaused()) {
                inGameUI.SetActive(false);
            } else {
                inGameUI.SetActive(true);
            }
            time += Time.deltaTime;
            score += Time.deltaTime;
            timeText.text = string.Format ("<i>{0:N2}</i>", time);
            panel.color = new Color32(255, 0, 0, (byte) ( (player.maxDurability - player.durability) * 100 / player.maxDurability ));
            scoreText.text = string.Format ("<i>Score: {0:N2}</i>", score);
        }
    }

    public void ScoreUp(float delta) {
        score += delta;
    }
}
