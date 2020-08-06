using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public Image image;
    public Text scoreText;
    public Text timeText;
    public GameObject retryBtn;
    public GameObject mainMenuBtn;
    // Start is called before the first frame update
    void Start()
    {
        Color32 zeroOpacityWhite = new Color(255, 255, 255, 0);
        image.color = zeroOpacityWhite;
        scoreText.color = zeroOpacityWhite;
        timeText.color = zeroOpacityWhite;
        retryBtn.SetActive(false);
        mainMenuBtn.SetActive(false);
        timeText.text = string.Format ("<i>Time: {0:N2} seconds</i>", GameManager.instance.timeOnDead);
        scoreText.text = string.Format ("<i>Score: {0:N2}</i>", GameManager.instance.scoreOnDead);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (image.color.a < 1)
            IncreaseImageAlpha();
        else {
            if (scoreText.color.a < 1)
                IncreaseTextAlpha();
            else {
                retryBtn.SetActive(true);
                mainMenuBtn.SetActive(true);
            }
        }
    }

    void IncreaseImageAlpha() {
        Color tempColor = image.color;
        tempColor.a += 0.01f;
        image.color = tempColor;
    }

    void IncreaseTextAlpha() {
        Color tempColor = scoreText.color;
        tempColor.a += 0.01f;
        scoreText.color = tempColor;
        timeText.color = tempColor;
    }
}
