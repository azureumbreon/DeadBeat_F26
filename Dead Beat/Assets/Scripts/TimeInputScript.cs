/*
Will Ziegler
11/4/2026
1.0

Handles note timers and time-based inputs/
*/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class TimeInputScript : MonoBehaviour
{
    //Timer variables
    //private int state;
    private List<ObstacleData> notes = new List<ObstacleData>();
    private int index;
    private float timeFinal;

    //UI Variables
    //public Button startButton;
    public TMP_Text scoreText;
    public TMP_Text roundScoreText;
    //public Button quitButton;
    //public GameObject menuPanel;

    //Score variables
    private int score;
    private float scoreAdd;
    private int roundScore;

    //Event
    public UnityEvent startGameEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //state = 0;
        score = 0;
        index = 0;

        //startButton.onClick.AddListener(StartTimer);
        //quitButton.onClick.AddListener(Quit);

        roundScoreText.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Score: " + score);
    }

    void StartTimer()
    {
        //state = 1;
        //menuPanel.SetActive(false);
    }

    void OnJump(InputValue moveVal)
    {
        if(index < notes.Count)
        {
            timeFinal = notes[index].GetTimerFinal();

            if (timeFinal <= 0f || timeFinal >= 2f) {
                roundScore = 0;
            }
            else if (timeFinal <= 1.5f) {
                roundScore = (int)(100f * Mathf.Pow(timeFinal / 1.5f, 2f));
            }   
            else {
                roundScore = (int)(100f * Mathf.Pow((2f - timeFinal) / 0.5f, 2f));
            }
            score += roundScore;

            //Destroy(notes[index].gameObject);
            index++;
            StartCoroutine(RoundScoreAnimate());
        }
    }

    IEnumerator RoundScoreAnimate()
    {
        roundScoreText.SetText("+" + roundScore);

        yield return new WaitForSeconds(1.0f);

        roundScoreText.SetText("");
    }

    public void AddObstacle(GameObject obstacleObj)
    {
        notes.Add(obstacleObj.GetComponent<ObstacleData>());
    }

    void Quit()
    {
        Application.Quit();
    }

    //public int GetState() { return state; }
}
