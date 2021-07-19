using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HockeyScripts.MenuScene
{
    public class MenuManager : MonoBehaviour
    {
        public Toggle multiPlayerToggle;
        public GameObject difficultyToggles;

        private void Start()
        {
            difficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("main");
        }

        #region Difficulty
        public void SetEasyDifficulty(bool isOn)
        {
            if (isOn)
                GameValues.Difficulty = GameValues.Difficulties.Easy;
        }
        public void SetMediumDifficulty(bool isOn)
        {
            if (isOn)
                GameValues.Difficulty = GameValues.Difficulties.Medium;
        } 
        public void SetHardDifficulty(bool isOn)
        {
            if (isOn)
                GameValues.Difficulty = GameValues.Difficulties.Hard;
        }  
        #endregion

    }
}