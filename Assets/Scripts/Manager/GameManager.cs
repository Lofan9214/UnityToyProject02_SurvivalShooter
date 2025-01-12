using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public PlayerInput input;

    private bool paused;
    private float fixedDeltaTimeInit;

    private int score;

    private void Start()
    {
        fixedDeltaTimeInit = 0.02f;
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void Resume(bool button = false)
    {
        if (paused || button)
        {
            paused = false;
            
            Time.timeScale = 1f;
        }
        else
        {
            paused = true;

            Time.timeScale = 0f;
        }
        uiManager.Pause(paused);

        input.enabled = !paused;
    }
    
    public void AddScore(int add)
    {
        score += add;
        uiManager.UpdateScoreText(score);
    }

    public IEnumerator OnDie()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}