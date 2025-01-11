using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public PlayerInput input;

    private bool paused;
    private float fixedDeltaTimeInit;

    private int score;


    private void Start()
    {
        fixedDeltaTimeInit = Time.fixedDeltaTime;
        score = 0;
        paused = false;
        uiManager.quitButton.onClick.AddListener(Quit);
        uiManager.resumeButton.onClick.AddListener(() => Resume(true));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume(bool button = false)
    {
        if (paused || button)
        {
            paused = false;
            
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fixedDeltaTimeInit;
        }
        else
        {
            paused = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
        }
        uiManager.Pause(paused);

        input.enabled = paused;
    }
    
    public void AddScore(int add)
    {
        score += add;
        uiManager.UpdateScoreText(score);
    }
}