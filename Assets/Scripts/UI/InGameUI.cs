using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject inGameUI;
    public Text timeText;
    public Image panel;
    private CoreCell player;
    private float time;

    // Start is called before the first frame update
    void Start()
    {  
        time = 0;
        player = GameObject.FindWithTag("PlayerCoreCell").GetComponent<CoreCell>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GetPaused()) {
            inGameUI.SetActive(false);
        } else {
            inGameUI.SetActive(true);
        }
        time += Time.deltaTime;
        timeText.text = string.Format ("<i>You survived {0:N2} seconds</i>", time);
        panel.color = new Color32(255, 0, 0, (byte) ( (player.maxDurability - player.durability) * 100 / player.maxDurability ));
    }
}
