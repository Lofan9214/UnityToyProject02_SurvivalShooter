using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDisplay = Animator.StringToHash("Display");
    private const string scoreFormat = "SCORE: {0}";

    public TextMeshProUGUI scoreText;
    public Button quitButton;
    public Button resumeButton;
    public Slider hpBar;
    public GameObject pauseWindow;
    public GameObject diePanel;

    public Animator damageEffecter;

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

    public void OnHit()
    {
        damageEffecter.SetTrigger(hashHit);
    }

    public void OnPlayerDie()
    {
        diePanel.SetActive(true);
        diePanel.GetComponent<Animator>().SetTrigger(hashDisplay);
    }
}
