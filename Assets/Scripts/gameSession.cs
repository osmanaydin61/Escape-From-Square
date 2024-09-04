using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int Score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI ScoreText;
 

         void Start() {
            livesText.text = playerLives.ToString();
            ScoreText.text = Score.ToString();
        }
        void Awake() 
        {
        int numGameSessions = FindObjectsOfType<gameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
        }    
        public void AddtoScore(int pointtoAdd)
        {
            Score += pointtoAdd;
            ScoreText.text = Score.ToString();
        }

         public void ProcessPlayerDeath()
        {
            if (playerLives>1)
            {
                TakeLife();
                
            }
            else 
            {
                resetGameSession();
            }
        }
        void TakeLife()
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString();
        }
        void resetGameSession()
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);

        }


    

    
    
}