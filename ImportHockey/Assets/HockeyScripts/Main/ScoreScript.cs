using UnityEngine;
using UnityEngine.UI;

namespace HockeyScripts.Main
{
    public class ScoreScript : MonoBehaviour
    {
        public enum Score
        {
            AIScore, PlayerScore
        }
        public Text aıScoreTxt;
       public Text playerScoreTxt;

        public UiManager uiManager;

        public int maxScore;

        #region Scores
        private int airScore, playerScore;

        private int AirScore
        {
            get => airScore;
            set
            {
                airScore = value;
                if (value == maxScore)
                    uiManager.ShowRestartCanvas(true);
            }
        }

        private int PlayerScore
        {
            get => playerScore;
            set
            {
                playerScore = value;
                if (value == maxScore)
                    uiManager.ShowRestartCanvas(false);
            }
        }
        #endregion

        public void Increment(Score whichScore)//Not static method so we can not use static func. 
        {
            if (whichScore == Score.AIScore)
                aıScoreTxt.text = (++AirScore).ToString();
            else
                playerScoreTxt.text = (++PlayerScore).ToString();
        }

        public void ResetScores()
        {
            AirScore = PlayerScore = 0;
            aıScoreTxt.text = playerScoreTxt.text = "0";

        }
    }
}
