using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const string scoreFormat = "SCORE: {0}";

    public TextMeshProUGUI scoreText;
    public Button quitButton;
    public Button resumeButton;
    public Slider hpBar;
    public GameObject pauseWindow;

    public void UpdateScoreText(int score)
    {
        scoreText.text = string.Format(scoreFormat, score);
    }

    public void Pause(bool pause)
    {
        pauseWindow.SetActive(pause);
    }

    public void UpdateHpBar(float hpRate)
    {
        hpBar.value = hpRate;
    }
}
