using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 120;
    public int difficulty = 1;
    int score = 0;
    public bool gameOver = false;
    public int Score {
        get => score;
        set {
            score = value;
            UIManager.Instance.UpdateUIScore(score);
            if(score % 1000==0)
                difficulty++;
        }
    }
    private void Awake()
    {
        if (Instance == null) {

            Instance = this;
        }
    }

    private void Start()
    {
      
        StartCoroutine(CountdownRoutine());
        UIManager.Instance.UpdateUITime(time);
    }
    IEnumerator CountdownRoutine() {
        while (time>0) {
            yield return new WaitForSeconds(1);
            difficulty++;
            time--;
            UIManager.Instance.UpdateUITime(time);
        }
        gameOver = true;
        UIManager.Instance.ShowGameOverScreen();

    }
    public void PlayAgain() {
        SceneManager.LoadScene("Game");
    }
}
