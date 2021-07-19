using System.Collections;
using HockeyScripts.Types;
using UnityEngine;

namespace HockeyScripts.Main
{
    public class PuckScript : MonoBehaviour ,IResettable
    {
        public ScoreScript scoreScriptInstance;
        public static bool WasGoal { get; private set; }
        public float maxSpeed;
        private Rigidbody2D rb;

        public AudioManager audioManager;
        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            WasGoal = false;
            UiManager.Instance.ResettableGameObjects.Add(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (WasGoal) return;
            switch (other.tag)
            {
                case "AIGoal":
                    scoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                    WasGoal = true;
                    audioManager.PlayGoal();
                    StartCoroutine(ResetPuck(false));
                    break;
                case "PlayerGoal":
                    scoreScriptInstance.Increment(ScoreScript.Score.AIScore);
                    WasGoal = true;
                    audioManager.PlayGoal();
                    StartCoroutine(ResetPuck(true));
                    break;
            }
        }
        private void OnCollisionEnter2D()
        {
            audioManager.PlayPuckCollision();
        }

        private IEnumerator ResetPuck(bool didAIScore)
        {
            yield return new WaitForSecondsRealtime(1);
            WasGoal = false;
            rb.velocity = rb.position = new Vector2(0, 0);

            rb.position = didAIScore ? new Vector2(0, -1) : new Vector2(0, 1);

        }

        public void ResetPosition()
        {
            rb.position = new Vector2(0, 0);
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
