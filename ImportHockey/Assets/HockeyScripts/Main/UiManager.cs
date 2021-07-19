using System.Collections.Generic;
using HockeyScripts.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HockeyScripts.Main
{
    public class UiManager : MonoBehaviour
    {
        #region Singleton
        public static UiManager Instance {get; private set;}
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        [Header("Canvas")]
        public GameObject canvasGame;
        public GameObject canvasRestart;

        [Header("CanvasRestart")]
        public GameObject winTxt;
        public GameObject lostTxt;

        [Header("Other")]
        public AudioManager audioManager; 
    
        public ScoreScript scoreScript;

        public readonly List<IResettable> ResettableGameObjects = new List<IResettable>();

        public void ShowRestartCanvas(bool didAIWin)
        {
            Time.timeScale = 0;

            canvasGame.SetActive(false);
            canvasRestart.SetActive(true);

            if (didAIWin)
            {
                audioManager.PlayLostGame();
                winTxt.SetActive(false);
                lostTxt.SetActive(true);
            }
            else
            {
                audioManager.PlayWonGame();
                winTxt.SetActive(true);
                lostTxt.SetActive(false);

            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1;

            canvasGame.SetActive(true);
            canvasRestart.SetActive(false);

            scoreScript.ResetScores();

            foreach (var obj in ResettableGameObjects)
                obj.ResetPosition();
        }

        public void ShowMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("menu");
        }
        
    }
}
